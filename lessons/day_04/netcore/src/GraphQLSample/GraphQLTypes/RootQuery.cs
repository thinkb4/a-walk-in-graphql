using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;
using System.Collections.Generic;
using System.Linq;

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

         Field<ListGraphType<SkillType>>("skills",
            arguments: new QueryArguments
            {
                   new  QueryArgument<InputSkillType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputSkill>("input");
               return input != default
                  ? AsList(_skillRepository.Get(input))
                  : _skillRepository.GetAll();
            });

         Field<SkillType>("randomSkill", resolve: context => _skillRepository.GetRandom());

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

         Field<ListGraphType<PersonType>>("persons",
            arguments: new QueryArguments
            {
               new  QueryArgument<InputPersonType> {  Name = "input" }
            },
            resolve: context =>
            {
               var input = context.GetArgument<InputPerson>("input");
               return input != default
                  ? AsList(_personRepository.Get(input))
                  : _personRepository.GetAll();
            });

         Field<PersonType>("randomPerson", resolve: context => _personRepository.GetRandom());

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

      private IEnumerable<T> AsList<T>(T item)
         where T : class
      {
         return item == default
            ? new List<T>()
            : Enumerable.Repeat(item, 1)
            ;
      }
   }
}
