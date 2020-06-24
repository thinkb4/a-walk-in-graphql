using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLNetCore.Models
{
   public class Skill
   {
      public string id { get; set; }
      public string name { get; set; }
      public string parentId { get; set; }
      public virtual Skill parent { get; set; }
   }

   public class SkillData
   {
      public string id { get; set; }
      public string name { get; set; }
      public string parent { get; set; }

      public static Skill ToEntity(SkillData data)
      {
         if (data == default)
            return default;

         return new Skill()
         {
            id = data.id,
            name = data.name,
            parentId = data.parent
         };
      }
   }
}
