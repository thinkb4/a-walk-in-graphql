using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;
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

      public Skill Get(InputSkill input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Skill.FirstOrDefault(input?.Predicate ?? (_ => false));
         }
      }

      public List<Skill> GetAll(InputSkill input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            return db.Skill.Where(input?.Predicate ?? (_ => true)).ToList();
         }
      }

      public Skill CreateSkill(InputSkillCreate input)
      {
         using (var scope = _scopeFactory.CreateScope())
         using (var db = scope.ServiceProvider.GetRequiredService<GraphQLContext>())
         {
            var skill = input.ToSkill();
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
