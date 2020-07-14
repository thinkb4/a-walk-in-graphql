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
         Field<NonNullGraphType<SkillType>>("randomSkill", resolve: context => _skillRepository.GetRandom());

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
