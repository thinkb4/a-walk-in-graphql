using System;

namespace GraphQLNetCore.Models
{
   public class InputPerson
   {
      public int? Id { get; set; }
      public int? Age { get; set; }
      public EyeColor? EyeColor { get; set; }
      public int? FavSkill { get; set; }

      public static InputPerson FromId(int? id) => id.HasValue ? new InputPerson { Id = id } : default;

      public Func<Person, bool> Predicate => 
         (person) => (!Id.HasValue || Id.Value == person.Id)
                  && (!Age.HasValue || Age.Value == person.Age)
                  && (!EyeColor.HasValue || EyeColor.Value == person.EyeColor)
                  && (!FavSkill.HasValue || FavSkill.Value == person.FavSkillId)
                  ;
   }
}
