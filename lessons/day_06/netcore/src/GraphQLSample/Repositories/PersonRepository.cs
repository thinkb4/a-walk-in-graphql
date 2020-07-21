using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;
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
                     .Include(x => x.friends)
                     .FirstOrDefault(s => s.id == personId)
                     ?.friends
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
                     .Include(x => x.skills)
                     .FirstOrDefault(s => s.id == personId)
                     ?.skills
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
            person.friends = db.Person.Where(p => input.friends.Contains(p.id)).ToList();
            person.skills = db.Skill.Where(s => input.skills.Contains(s.id)).ToList();
            db.Person.Add(person);
            db.SaveChanges();
            return person;
         }
      }
   }
}
