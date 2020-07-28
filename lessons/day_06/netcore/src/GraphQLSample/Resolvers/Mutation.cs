using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    public class Mutation
    {
        private readonly ISkillRepository _skills;
        private readonly IPersonRepository _persons;

        public Mutation(ISkillRepository skills, IPersonRepository person)
        {
            _skills = skills;
            _persons = person;
        }

        public Skill CreateSkill(InputSkillCreate input) => _skills.CreateSkill(input);
        public Person CreatePerson(InputPersonCreate input) => _persons.CreatePerson(input);
        public Person CreateCandidate(InputCandidateCreate input) => _persons.CreatePerson(input);
        public Person CreateEngineer(InputEngineerCreate input) => _persons.CreatePerson(input);
    }
}
