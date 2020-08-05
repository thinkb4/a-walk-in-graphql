using System.Collections.Generic;
using GraphQL;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    [GraphQLMetadata(nameof(Contact), IsTypeOf = typeof(Contact))]
    public class ContactResolver
    {
        private readonly IPersonRepository _persons;
        private readonly ISkillRepository _skills;

        public ContactResolver(IPersonRepository persons, ISkillRepository skills)
        {
            this._persons = persons;
            this._skills = skills;
        }

        public string FullName(Contact contact) => $"{contact.Name} {contact.Surname}";
        public Skill FavSkill(Contact contact) => _skills.Get(InputSkill.FromId(contact.FavSkillId));
        public List<Skill> Skills(Contact contact, InputSkill input) => _persons.GetSkills(contact.Id, input);
        public List<Person> Friends(Contact contact, InputPerson input) => _persons.GetFriends(contact.Id, input);
    }
}
