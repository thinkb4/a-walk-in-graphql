using GraphQL.Types;
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

         Name = "Query";
         Field<ListGraphType<NonNullGraphType<SkillType>>>("skills",
            arguments: new QueryArguments
            {
                   new  QueryArgument<IdGraphType> {  Name = "id" }
            },
            resolve: context =>
            {
               var id = context.GetArgument<int?>("id");
               return id.HasValue
                  ? AsList(_skillRepository.Get(id))
                  : _skillRepository.GetAll();
            });

         Field<NonNullGraphType<SkillType>>("randomSkill", resolve: context => _skillRepository.GetRandom());

         Field<SkillType>("skill",
             arguments: new QueryArguments
             {
                   new  QueryArgument<IdGraphType> {  Name = "id" }
             },
             resolve: context =>
             {
                var id = context.GetArgument<int?>("id");
                return _skillRepository.Get(id);
             });

         Field<ListGraphType<NonNullGraphType<PersonType>>>("persons",
            arguments: new QueryArguments
            {
               new  QueryArgument<IdGraphType> {  Name = "id" }
            },
            resolve: context =>
            {
               var id = context.GetArgument<int?>("id");
               return id.HasValue
                  ? AsList(_personRepository.Get(id))
                  : _personRepository.GetAll();
            });

         Field<NonNullGraphType<PersonType>>("randomPerson", resolve: context => _personRepository.GetRandom());

         Field<PersonType>("person",
             arguments: new QueryArguments
             {
                   new  QueryArgument<IdGraphType> {  Name = "id" }
             },
             resolve: context =>
             {
                var id = context.GetArgument<int?>("id");
                return _personRepository.Get(id);
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
