using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLNetCore.Repositories
{
   public class PersonRepository : IPersonRepository
   {
      private readonly IServiceScopeFactory _scopeFactory;
      public PersonRepository(IServiceScopeFactory scopeFactory)
      {
         _scopeFactory = scopeFactory;
      }

      public Person Get(InputPerson input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.FirstOrDefault(input?.Predicate ?? (_ => false));
         }
      }

      public List<Person> GetAll(InputPerson input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.Where(input?.Predicate ?? (_ => true)).ToList();
         }
      }

      public List<Person> GetFriends(int personId, InputPerson input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person
                     .Include(x => x.Friends)
                     .FirstOrDefault(s => s.Id == personId)
                     ?.Friends
                     .Where(input?.Predicate ?? (_ => true))
                     .ToList()
                     ;
         }
      }

      public List<Skill> GetSkills(int personId, InputSkill input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person
                     .Include(x => x.Skills)
                     .FirstOrDefault(s => s.Id == personId)
                     ?.Skills
                     .Where(input?.Predicate ?? (_ => true))
                     .ToList()
                     ;
         }
      }

      public Person GetRandom()
      {
         var rng = new System.Random();
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var indexRandom = rng.Next(db.Person.Count());
            return db.Person.Skip(indexRandom).FirstOrDefault();
         }
      }

      public Person CreatePerson(InputPersonCreate input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var person = input.ToPerson();
            person.Friends = db.Person.Where(p => input.Friends.Contains(p.Id)).ToList();
            person.Skills = db.Skill.Where(s => input.Skills.Contains(s.Id)).ToList();
            db.Person.Add(person);
            db.SaveChanges();
            return person;
         }
      }

      public Candidate CreateCandidate(InputCandidateCreate input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var candidate = input.ToPerson();
            candidate.Friends = db.Person.Where(p => input.Friends.Contains(p.Id)).ToList();
            candidate.Skills = db.Skill.Where(s => input.Skills.Contains(s.Id)).ToList();
            db.Person.Add(candidate);
            db.SaveChanges();
            return candidate;
         }
      }

      public Engineer CreateEngineer(InputEngineerCreate input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var engineer = input.ToPerson();
            engineer.Friends = db.Person.Where(p => input.Friends.Contains(p.Id)).ToList();
            engineer.Skills = db.Skill.Where(s => input.Skills.Contains(s.Id)).ToList();
            db.Person.Add(engineer);
            db.SaveChanges();
            return engineer;
         }
      }
   }
}
