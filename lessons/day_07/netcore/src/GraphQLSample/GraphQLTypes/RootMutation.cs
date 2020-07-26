using GraphQL;
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

         Field<NonNullGraphType<PersonInterface>>("createPerson",
            arguments: new QueryArguments
            {
               new  QueryArgument<NonNullGraphType<InputPersonCreateType>> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPersonCreate>("input");
               return _personRepository.CreatePerson(input);
            });

         Field<NonNullGraphType<CandidateType>>("createCandidate",
           arguments: new QueryArguments
           {
               new  QueryArgument<NonNullGraphType<InputCandidateCreateType>> {  Name = "input" }
           },
           resolve: context =>
           {
              var input = context.GetArgument<InputCandidateCreate>("input");
              return _personRepository.CreatePerson(input);
           });

         Field<NonNullGraphType<EngineerType>>("createEngineer",
            arguments: new QueryArguments
            {
               new  QueryArgument<NonNullGraphType<InputEngineerCreateType>> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputEngineerCreate>("input");
               return _personRepository.CreatePerson(input);
            });
      }
   }
}
