namespace GraphQLNetCore.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Skill Parent { get; set; }
    }
}
