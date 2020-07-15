using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface ISkillRepository
   {
      Skill Get(InputSkill input);
      List<Skill> GetAll(InputSkill input);
      Skill CreateSkill(InputSkillCreate input);
      Skill GetRandom();
   }
}
