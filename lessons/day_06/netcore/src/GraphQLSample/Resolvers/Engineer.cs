using System.Collections.Generic;
using GraphQL;
using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    [GraphQLMetadata(nameof(Engineer), IsTypeOf = typeof(Engineer))]
    public class EngineerResolver
    {
        private readonly IPersonRepository _persons;
        private readonly ISkillRepository _skills;

        public EngineerResolver(IPersonRepository persons, ISkillRepository skills)
        {
            this._persons = persons;
            this._skills = skills;
        }

        public string FullName(Engineer engineer) => $"{engineer.Name} {engineer.Surname}";
        public Skill FavSkill(Engineer engineer) => _skills.Get(InputSkill.FromId(engineer.FavSkillId));
        public List<Skill> Skills(Engineer engineer, InputSkill input) => _persons.GetSkills(engineer.Id, input);
        public List<Person> Friends(Engineer engineer, InputPerson input) => _persons.GetFriends(engineer.Id, input);
    }
}
