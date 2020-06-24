using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface ISkillRepository
   {
      Skill Get(string id);
      List<Skill> GetAll();
      Skill AddSkill(Skill skill);
      Skill GetRandom();
   }
}
