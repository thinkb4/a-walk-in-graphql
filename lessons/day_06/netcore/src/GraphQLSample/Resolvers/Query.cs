using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<object> Search(InputGlobalSearch input)
        {
            var skills = _skills.GetAll(new InputSkill { Name = input.Name });
            var persons = _persons.GetAll(new InputPerson { Name = input.Name });
            return persons.AsEnumerable<object>().Union(skills);
        }
    }
}
