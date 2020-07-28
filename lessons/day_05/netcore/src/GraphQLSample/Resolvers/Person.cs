using System.Collections.Generic;
using GraphQL;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    [GraphQLMetadata(nameof(Person), IsTypeOf = typeof(Person))]
    public class PersonResolver
    {
        private readonly IPersonRepository _persons;
        private readonly ISkillRepository _skills;

        public PersonResolver(IPersonRepository persons, ISkillRepository skills)
        {
            this._persons = persons;
            this._skills = skills;
        }

        public string FullName(Person person) => $"{person.Name} {person.Surname}";
        public Skill FavSkill(Person person) => _skills.Get(InputSkill.FromId(person.FavSkillId));
        public List<Skill> Skills(Person person, InputSkill input) => _persons.GetSkills(person.Id, input);
        public List<Person> Friends(Person person, InputPerson input) => _persons.GetFriends(person.Id, input);
    }
}
