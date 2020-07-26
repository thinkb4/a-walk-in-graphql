namespace GraphQLNetCore.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Skill Parent { get; set; }
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
                Id = int.Parse(data.id),
                Name = data.name,
                ParentId = int.TryParse(data.parent, out int parsedValue) ? parsedValue : default(int?)
            };
        }
    }
}
