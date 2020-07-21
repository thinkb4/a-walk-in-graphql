using System;
using System.Collections.Generic;

namespace GraphQLNetCore.Models
{
   public class InputPersonCreate
   {
      public string name { get; set; }
      public string surname { get; set; }
      public string email { get; set; }
      public int? age { get; set; }
      public EyeColor? eyeColor { get; set; }
      public List<int> friends { get; set; }
      public List<int> skills { get; set; }
      public int? favSkill { get; set; }

      internal Person ToPerson()
      {
         return new Person() {
            age = age ?? 0,
            email = email ?? String.Empty,
            eyeColor = eyeColor ?? EyeColor.BLUE,
            favSkillId = favSkill,
            name = name,
            surname = surname ?? String.Empty
         };
      }
   }
}
