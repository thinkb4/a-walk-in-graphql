using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface IPersonRepository
   {
      List<Person> GetAll(InputPerson input);
      Person Get(InputPerson input);
      List<Person> GetFriends(int personId, InputPerson input);
      List<Skill> GetSkills(int personId, InputSkill input);
      Person GetRandom();
   }
}
