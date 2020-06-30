using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface IPersonRepository
   {
      List<Person> GetAll();
      Person Get(int? id);
      List<Person> GetFriends(int id);
      List<Skill> GetSkills(int id);
      Person GetRandom();
   }
}
