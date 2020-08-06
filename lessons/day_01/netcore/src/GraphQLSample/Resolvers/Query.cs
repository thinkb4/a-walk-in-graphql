using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    public class Query
    {
        private readonly ISkillRepository _skills;
        public Query(ISkillRepository skills)
        {
            _skills = skills;
        }

        public Skill RandomSkill() => _skills.GetRandom();
    }
}
