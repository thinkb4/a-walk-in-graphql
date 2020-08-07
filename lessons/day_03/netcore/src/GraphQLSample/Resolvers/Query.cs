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
        public Skill Skill(int? id) => _skills.Get(id);
        public Person Person(int? id) => _persons.Get(id);
        public List<Skill> Skills(int? id) => _skills.GetAll(id);
        public List<Person> Persons(int? id) => _persons.GetAll(id);
    }
}
