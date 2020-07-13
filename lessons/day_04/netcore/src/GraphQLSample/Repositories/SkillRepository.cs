using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLNetCore.Repositories
{
   public class SkillRepository : ISkillRepository
   {
      private readonly IServiceScopeFactory _scopeFactory;
      public SkillRepository(IServiceScopeFactory scopeFactory)
      {
         _scopeFactory = scopeFactory;
      }

      public Skill Get(int? id)
      {
         if (id.HasValue)
         {
            using (var scope = _scopeFactory.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
            {
               return db.Skill.FirstOrDefault(s => s.id == id);
            }
         }
         return null;
      }

      public List<Skill> GetAll()
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Skill.ToList();
         }
      }

      public Skill AddSkill(Skill skill)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            db.Skill.Add(skill);
            db.SaveChanges();
            return skill;
         }
      }

      public Skill GetRandom()
      {
         var rng = new System.Random();
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var indexRandom = rng.Next(db.Skill.Count());
            return db.Skill.Skip(indexRandom).FirstOrDefault();
         }
      }

   }
}
