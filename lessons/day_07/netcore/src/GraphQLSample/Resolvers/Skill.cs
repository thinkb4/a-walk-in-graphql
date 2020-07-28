using System;
using GraphQL;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    [GraphQLMetadata(nameof(Skill), IsTypeOf = typeof(Skill))]
    public class SkillResolver
    {
        private readonly ISkillRepository _repo;

        public SkillResolver(ISkillRepository repo)
        {
            this._repo = repo;
        }
        
        public DateTime Now() => DateTime.Now; 
        public Skill Parent(Skill skill) => _repo.Get(InputSkill.FromId(skill.ParentId));
    }
}
