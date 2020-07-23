using System.Collections.Generic;

namespace GraphQLNetCore.Models
{
    public class Person
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<Person> Friends { get; set; }
        public List<Skill> Skills { get; set; }
        public Skill FavSkill { get; set; }
        public int? FavSkillId { get; set; }
    }

    public class PersonData
    {
        public string id { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }
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

            return new Person()
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
