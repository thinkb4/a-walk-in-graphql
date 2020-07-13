using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface IPersonRepository
   {
      List<Person> GetAll();
      Person Get(int? id);
      List<Person> GetFriends(int personId, int? id);
      List<Skill> GetSkills(int personId, int? id);
      Person GetRandom();
   }
}
