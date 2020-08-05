namespace GraphQLNetCore.Models.Input
{
   public class InputSkillCreate
   {
      public string Name { get; set; }

      public int? Parent { get; set; }

      internal Skill ToSkill()
      {
         return new Skill { 
            Name = Name,
            ParentId = Parent
         };
      }
   }
}
