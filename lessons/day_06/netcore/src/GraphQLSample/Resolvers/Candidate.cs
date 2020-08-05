using System.Collections.Generic;
using GraphQL;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    [GraphQLMetadata(nameof(Candidate), IsTypeOf = typeof(Candidate))]
    public class CandidateResolver
    {
        private readonly IPersonRepository _persons;
        private readonly ISkillRepository _skills;

        public CandidateResolver(IPersonRepository persons, ISkillRepository skills)
        {
            this._persons = persons;
            this._skills = skills;
        }

        public string FullName(Candidate candidate) => $"{candidate.Name} {candidate.Surname}";
        public Skill FavSkill(Candidate candidate) => _skills.Get(InputSkill.FromId(candidate.FavSkillId));
        public List<Skill> Skills(Candidate candidate, InputSkill input) => _persons.GetSkills(candidate.Id, input);
        public List<Person> Friends(Candidate candidate, InputPerson input) => _persons.GetFriends(candidate.Id, input);
    }
}
