using GraphQLNetCore.Models.Enums;
using System;

namespace GraphQLNetCore.Models.Input
{
   public class InputPerson
   {
      public int? Id { get; set; }
      public string Name { get; set; }
      public int? Age { get; set; }
      public EyeColor? EyeColor { get; set; }
      public int? FavSkill { get; set; }
      public Role? TargetRole { get; set; }
      public Grade? TargetGrade { get; set; }
      public Role? Role { get; set; }
      public Grade? Grade { get; set; }

      public static InputPerson FromId(int? id) => id.HasValue ? new InputPerson { Id = id } : default;

      public Func<Person, bool> Predicate =>
         (person) => (!Id.HasValue || Id.Value == person.Id)
                  && (!Age.HasValue || Age.Value == person.Age)
                  && (string.IsNullOrEmpty(Name) || person.Name.Contains(Name) || person.Surname.Contains(Name))
                  && (!EyeColor.HasValue || EyeColor.Value == person.EyeColor)
                  && (!FavSkill.HasValue || FavSkill.Value == person.FavSkillId)
                  && (!TargetRole.HasValue || TargetRole.Value == (person as Candidate)?.TargetRole)
                  && (!TargetGrade.HasValue || TargetGrade.Value == (person as Candidate)?.TargetGrade)
                  && (!Role.HasValue || Role.Value == (person as Engineer)?.Role)
                  && (!Grade.HasValue || Grade.Value == (person as Engineer)?.Grade)
                  ;
   }
}
