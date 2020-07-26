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
               return db.Person.FirstOrDefault(s => s.Id == id);
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

      public List<Person> GetFriends(int personId)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person
                     .Include(x => x.Friends)
                     .FirstOrDefault(s => s.Id == personId)
                     ?.Friends
                     .ToList()
                     ;
         }
      }

      public List<Skill> GetSkills(int personId)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person
                     .Include(x => x.Skills)
                     .FirstOrDefault(s => s.Id == personId)
                     ?.Skills
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
