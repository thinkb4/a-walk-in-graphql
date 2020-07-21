using GraphQL.Introspection;
using System;

namespace GraphQLNetCore.Models
{
   public class InputSkill
   {
      public int? id { get; set; }
      public string name { get; set; }

      public static InputSkill FromId(int? id) => id.HasValue ? new InputSkill { id = id } : default;

      public Func<Skill, bool> Predicate => 
         (skill) => (!id.HasValue || id.Value == skill.id) && (String.IsNullOrEmpty(name) || skill.name.Contains(name));
   }
}
