using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;
using System.Collections.Generic;

namespace GraphQLNetCore.Resolvers
{
    public class Query
    {
        private readonly ISkillRepository _skills;
        private readonly IPersonRepository _persons;
        public Query(ISkillRepository skills, IPersonRepository persons)
        {
            _skills = skills;
            _persons = persons;
        }

        public List<Skill> Skills(InputSkill input) => _skills.GetAll(input);
        public Skill RandomSkill() => _skills.GetRandom();
        public Skill Skill(InputSkill input) => _skills.Get(input);
        public List<Person> Persons(InputPerson input) => _persons.GetAll(input);
        public Person RandomPerson => _persons.GetRandom();
        public Person Person(InputPerson input) => _persons.Get(input);
    }
}
