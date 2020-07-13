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

         Field<SkillType>("randomSkill", resolve: context => _skillRepository.GetRandom());
      }
   }
}
