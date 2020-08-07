using GraphQLNetCore.Models;
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

        public Skill RandomSkill() => _skills.GetRandom();
        public Person RandomPerson => _persons.GetRandom();
        public List<Person> Persons(int? id) => _persons.GetAll(id);
    }
}
