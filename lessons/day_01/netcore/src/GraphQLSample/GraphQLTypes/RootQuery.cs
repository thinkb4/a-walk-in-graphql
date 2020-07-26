using GraphQL.Types;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootQuery : ObjectGraphType
   {
      private readonly ISkillRepository _skillRepository;

      public RootQuery(ISkillRepository skillRepository)
      {
         _skillRepository = skillRepository;

         Name = "Query";
         Field<NonNullGraphType<SkillType>>("randomSkill", resolve: context => _skillRepository.GetRandom());
      }
   }
}
