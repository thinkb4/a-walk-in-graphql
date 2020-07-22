using GraphQLNetCore.Models.Enums;
using System.Collections.Generic;

namespace GraphQLNetCore.Models.Input
{
   public class InputEngineerCreate
   {
      public string Name { get; set; }
      public string Surname { get; set; }
      public string Email { get; set; }
      public int Age { get; set; }
      public EyeColor? EyeColor { get; set; }
      public List<int> Friends { get; set; }
      public List<int> Skills { get; set; }
      public int? FavSkill { get; set; }
      public int EmployeeId { get; set; }
      public Role Role { get; set; }
      public Grade Grade { get; set; }

      internal Engineer ToPerson()
      {
         return new Engineer() {
            Age = Age,
            Email = Email,
            EyeColor = EyeColor,
            FavSkillId = FavSkill,
            Name = Name,
            Surname = Surname,
            EmployeeId = EmployeeId,
            Role = Role,
            Grade = Grade,
         };
      }
   }
}
