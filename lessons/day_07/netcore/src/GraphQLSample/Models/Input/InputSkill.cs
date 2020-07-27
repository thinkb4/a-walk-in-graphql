using System;

namespace GraphQLNetCore.Models.Input
{
    public class InputSkill
   {
      public int? Id { get; set; }
      public string Name { get; set; }

      public static InputSkill FromId(int? id) => id.HasValue ? new InputSkill { Id = id } : default;

      public Func<Skill, bool> Predicate => 
         (skill) => (!Id.HasValue || Id.Value == skill.Id) && (String.IsNullOrEmpty(Name) || skill.Name.Contains(Name));
   }
}
