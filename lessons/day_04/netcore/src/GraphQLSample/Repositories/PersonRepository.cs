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

      public Person Get(int? id)
      {
         if (id.HasValue)
         {
            using (var scope = _scopeFactory.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
            {
               return db.Person.FirstOrDefault(s => s.id == id);
            }
         }
         return null;
      }

      public List<Person> GetAll()
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.ToList();
         }
      }

      public List<Person> GetFriends(int personId, int? id)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person
                     .Include(x => x.friends)
                     .FirstOrDefault(s => s.id == personId)
                     ?.friends
                     .Where(f => !id.HasValue || id.Value == f.id)
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
   }
}
