using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface ISkillRepository
   {
      Skill Get(string id);
      Skill GetRandom();
   }
}
