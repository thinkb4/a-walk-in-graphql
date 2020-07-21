using GraphQLNetCore.Models.Enums;
using System.Collections.Generic;

namespace GraphQLNetCore.Models
{
   public class Person
   {
      public int Id { get; set; }
      public int Age { get; set; }
      public EyeColor EyeColor { get; set; }
      public string Name { get; set; }
      public string Surname { get; set; }
      public string Email { get; set; }
      public List<Person> Friends { get; set; }
      public List<Skill> Skills { get; set; }
      public Skill FavSkill { get; set; }
      public int? FavSkillId { get; set; }
   }
}
