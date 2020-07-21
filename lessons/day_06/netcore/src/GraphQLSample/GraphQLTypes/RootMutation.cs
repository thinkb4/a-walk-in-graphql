using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Output;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootMutation : ObjectGraphType
   {
      private readonly ISkillRepository _skillRepository;
      private readonly IPersonRepository _personRepository;

      public RootMutation(ISkillRepository skillRepository, IPersonRepository personRepository)
      {
         _skillRepository = skillRepository;
         _personRepository = personRepository;

         Name = "Mutation";
         Field<NonNullGraphType<SkillType>>("createSkill",
            arguments: new QueryArguments
            {
               new  QueryArgument<NonNullGraphType<InputSkillCreateType>> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkillCreate>("input");
               return _skillRepository.CreateSkill(input);
            });

         Field<NonNullGraphType<PersonType>>("createPerson",
            arguments: new QueryArguments
            {
               new  QueryArgument<NonNullGraphType<InputPersonCreateType>> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPersonCreate>("input");
               return _personRepository.CreatePerson(input);
            });
      }
   }
}
