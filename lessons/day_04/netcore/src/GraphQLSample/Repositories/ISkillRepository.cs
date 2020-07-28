using System.Collections.Generic;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.Repositories
{
   public interface ISkillRepository
   {
      Skill Get(InputSkill input);
      List<Skill> GetAll(InputSkill input);
      Skill GetRandom();
   }
}
