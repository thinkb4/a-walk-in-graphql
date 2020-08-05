using System.Collections.Generic;
using GraphQL;
using GraphQLNetCore.Models;
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
        public Skill FavSkill(Person person) => _skills.Get(person.FavSkillId);
        public List<Skill> Skills(Person person, int? id) => _persons.GetSkills(person.Id, id);
        public List<Person> Friends(Person person, int? id) => _persons.GetFriends(person.Id, id);
    }
}
