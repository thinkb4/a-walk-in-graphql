using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
   public interface IPersonRepository
   {
      List<Person> GetAll(int? id);
      List<Person> GetFriends(int personId);
      List<Skill> GetSkills(int personId);
      Person GetRandom();
   }
}
