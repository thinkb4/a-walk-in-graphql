using GraphQLNetCore.Models.Enums;
using System;
using System.Collections.Generic;

namespace GraphQLNetCore.Models.Input
{
   public class InputPersonCreate
   {
      public string Name { get; set; }
      public string Surname { get; set; }
      public string Email { get; set; }
      public int? Age { get; set; }
      public EyeColor? EyeColor { get; set; }
      public List<int> Friends { get; set; }
      public List<int> Skills { get; set; }
      public int? FavSkill { get; set; }

      internal Person ToPerson()
      {
         return new Person() {
            Age = Age ?? 0,
            Email = Email ?? string.Empty,
            EyeColor = EyeColor ?? Enums.EyeColor.BLUE,
            FavSkillId = FavSkill,
            Name = Name,
            Surname = Surname ?? string.Empty
         };
      }
   }
}
