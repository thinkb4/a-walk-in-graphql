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

         Field<SkillType>("randomSkill", resolve: context => _skillRepository.GetRandom());

         Field<ListGraphType<PersonType>>("persons",
            arguments: new QueryArguments
            {
               new  QueryArgument<IntGraphType> {  Name = "id" }
            },
            resolve: context =>
            {
               var id = context.GetArgument<int?>("id");
               return id.HasValue
                  ? AsList(_personRepository.Get(id))
                  : _personRepository.GetAll();
            });

         Field<PersonType>("randomPerson", resolve: context => _personRepository.GetRandom());
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
