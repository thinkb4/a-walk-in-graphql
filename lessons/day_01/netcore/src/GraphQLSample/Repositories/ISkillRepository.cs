using System.Collections.Generic;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.Repositories
{
    public interface ISkillRepository
    {
        List<Skill> GetAll();
        Skill AddSkill(Skill skill);
        Skill GetRandom();
    }
}
