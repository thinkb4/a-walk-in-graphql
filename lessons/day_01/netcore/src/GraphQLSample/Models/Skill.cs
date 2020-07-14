namespace GraphQLNetCore.Models
{
   public class Skill
   {
      public int id { get; set; }
      public string name { get; set; }
      public int? parentId { get; set; }
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
            id = int.Parse(data.id),
            name = data.name,
            parentId = int.TryParse(data.parent, out int parsedValue) ? parsedValue : default(int?)
         };
      }
   }
}
