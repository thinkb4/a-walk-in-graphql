using System;

namespace GraphQLNetCore.Models
{
   public class InputPerson
   {
      public int? id { get; set; }
      public int? age { get; set; }
      public EyeColor? eyeColor { get; set; }
      public int? favSkill { get; set; }

      public static InputPerson FromId(int? id) => new InputPerson { id = id };

      public Func<Person, bool> Predicate => 
         (person) => (!id.HasValue || id.Value == person.id)
                  && (!age.HasValue || age.Value == person.age)
                  && (!eyeColor.HasValue || eyeColor.Value == person.eyeColor)
                  && (!favSkill.HasValue || favSkill.Value == person.favSkillId)
                  ;
   }
}
