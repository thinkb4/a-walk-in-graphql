using GraphQLNetCore.Models.Enums;
using System.Collections.Generic;

namespace GraphQLNetCore.Models.Input
{
   public class InputCandidateCreate
   {
      public string Name { get; set; }
      public string Surname { get; set; }
      public string Email { get; set; }
      public int Age { get; set; }
      public EyeColor? EyeColor { get; set; }
      public List<int> Friends { get; set; }
      public List<int> Skills { get; set; }
      public int? FavSkill { get; set; }
      public Role TargetRole { get; set; }
      public Grade TargetGrade { get; set; }

      internal Candidate ToPerson()
      {
         return new Candidate() {
            Age = Age,
            Email = Email,
            EyeColor = EyeColor,
            FavSkillId = FavSkill,
            Name = Name,
            Surname = Surname,
            TargetRole = TargetRole,
            TargetGrade = TargetGrade
         };
      }
   }
}
