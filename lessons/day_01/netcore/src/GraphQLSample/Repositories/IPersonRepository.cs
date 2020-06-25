using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface IPersonRepository
   {
      List<Person> GetAll();
      Person Get(string id);
      List<Person> GetFriends(string id);
      List<Skill> GetSkills(string id);
      Person GetRandom();
   }
}
