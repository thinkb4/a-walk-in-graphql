using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLNetCore.Resolvers
{
    public class Query
   {
      private readonly ISkillRepository _skillRepository;
      private readonly IPersonRepository _personRepository;
      public Query(ISkillRepository skillRepository, IPersonRepository personRepository)
      {
         _skillRepository = skillRepository;
         _personRepository = personRepository;
      }

      public List<Skill> Skills(InputSkill input)
      {
         return _skillRepository.GetAll(input);
      }

      public Skill RandomSkill(ISkillRepository skillRepository)
      {
         return _skillRepository.GetRandom();
      }

      public Skill Skill(InputSkill input)
      {
         return _skillRepository.Get(input);
      }

      public List<Person> Persons(InputPerson input)
      {
         return _personRepository.GetAll(input);
      }

      public Person RandomPerson()
      {
         return _personRepository.GetRandom();
      }

      public Person Person(InputPerson input)
      {
         return _personRepository.Get(input);
      }

      public IEnumerable<object> Search(InputGlobalSearch input) {
         var skills = _skillRepository.GetAll(new InputSkill { Name = input.Name });
         var persons = _personRepository.GetAll(new InputPerson { Name = input.Name });
         return persons.AsEnumerable<object>().Union(skills);
      }
   }
}
