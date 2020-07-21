namespace GraphQLNetCore.Models
{
   public class InputSkillCreate
   {
      public string name { get; set; }

      public int? parent { get; set; }

      internal Skill ToSkill()
      {
         return new Skill { 
            name = name,
            parentId = parent
         };
      }
   }
}
