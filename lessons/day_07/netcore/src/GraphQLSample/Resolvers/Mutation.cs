using GraphQLNetCore.Models;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.Resolvers
{
    public class Mutation
   {
      private readonly ISkillRepository _skillRepository;
      private readonly IPersonRepository _personRepository;

      public Mutation(ISkillRepository skillRepository, IPersonRepository personRepository)
      {
         _skillRepository = skillRepository;
         _personRepository = personRepository;
      }

      public Skill CreateSkill(InputSkillCreate input) 
      {
         return _skillRepository.CreateSkill(input);
      }

      public Person CreatePerson(InputPersonCreate input) 
      {
         return _personRepository.CreatePerson(input);
      }

      public Person CreateCandidate(InputCandidateCreate input) 
      {
         return _personRepository.CreatePerson(input);
      }

      public Person CreateEngineer(InputEngineerCreate input) 
      {
         return _personRepository.CreatePerson(input);
      }
   }
}
