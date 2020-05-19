using System.Collections.Generic;
using System.Linq;
using GraphQLNetCore.Data;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
    public class SkillRepository: ISkillRepository
    {

        private readonly GraphQLContext _graphContext;
        public SkillRepository(GraphQLContext context)
        {
            _graphContext = context;
        }
        public List<Skill> GetAll()
        {
            return _graphContext.Skill.ToList();
        }

        public Skill AddSkill(Skill skill)
        {
            _graphContext.Skill.Add(skill);
            _graphContext.SaveChanges();
            return skill;
        }

        public Skill GetRandom()
        {
            var rng = new System.Random();
            var indexRandom = rng.Next(_graphContext.Skill.Count());
            return _graphContext.Skill.ToList()[indexRandom];
        }
    }
}
