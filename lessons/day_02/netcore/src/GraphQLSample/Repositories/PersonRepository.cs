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

      public Person Get(string id)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.FirstOrDefault(s => s.id == id);
         }
      }

      public List<Person> GetAll()
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.ToList();
         }
      }

      public List<Person> GetFriends(string id)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.Include(x => x.friends).FirstOrDefault(s => s.id == id)?.friends;
         }
      }

      public List<Skill> GetSkills(string id)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Person.Include(x => x.skills).FirstOrDefault(s => s.id == id)?.skills;
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
