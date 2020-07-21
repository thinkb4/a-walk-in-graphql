using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Output;
using GraphQLNetCore.Models.Input;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootQuery : ObjectGraphType
   {
      private readonly ISkillRepository _skillRepository;
      private readonly IPersonRepository _personRepository;

      public RootQuery(ISkillRepository skillRepository, IPersonRepository personRepository)
      {
         _skillRepository = skillRepository;
         _personRepository = personRepository;

         Name = "Query";
         Field<ListGraphType<NonNullGraphType<SkillType>>>("skills",
            arguments: new QueryArguments
            {
               new  QueryArgument<InputSkillType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkill>("input");
               return _skillRepository.GetAll(input);
            });

         Field<NonNullGraphType<SkillType>>("randomSkill", resolve: context => _skillRepository.GetRandom());

         Field<SkillType>("skill",
             arguments: new QueryArguments
             {
                new  QueryArgument<InputSkillType> {  Name = "input" }
             },
             resolve: context =>
             {
                var input = context.GetArgument<InputSkill>("input");
                return _skillRepository.Get(input);
             });

         Field<ListGraphType<NonNullGraphType<PersonType>>>("persons",
            arguments: new QueryArguments
            {
               new  QueryArgument<InputPersonType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPerson>("input");
               return _personRepository.GetAll(input);
            });

         Field<NonNullGraphType<PersonType>>("randomPerson", resolve: context => _personRepository.GetRandom());

         Field<PersonType>("person",
             arguments: new QueryArguments
             {
                new  QueryArgument<InputPersonType> {  Name = "input" }
             },
             resolve: context =>
             {
                var input = context.GetArgument<InputPerson>("input");
                return _personRepository.Get(input);
             });
      }
   }
}
