using GraphQLNetCore.Models.Enums;
using System.Collections.Generic;

namespace GraphQLNetCore.Models.Data
{
   public class PersonData
   {
      public string id { get; set; }
      public int age { get; set; }
      public EyeColor eyeColor { get; set; }
      public string name { get; set; }
      public string surname { get; set; }
      public string email { get; set; }
      public List<string> friends { get; set; }
      public List<string> skills { get; set; }
      public string favSkill { get; set; }

      public static Person ToEntity(PersonData data)
      {
         if (data == default)
            return default;

         return new Contact()
         {
            Id = int.Parse(data.id),
            Age = data.age,
            EyeColor = data.eyeColor,
            Email = data.email,
            FavSkillId = int.TryParse(data.favSkill, out int parsedValue) ? parsedValue : default(int?),
            Name = data.name,
            Surname = data.surname
         };
      }
   }
}
