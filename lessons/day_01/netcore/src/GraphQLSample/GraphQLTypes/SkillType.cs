using GraphQL.Types;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;

namespace GraphQLNetCore.GraphQLTypes
{
   public class SkillType : ObjectGraphType<Skill>
   {
      public SkillType(ISkillRepository repo)
      {
         Name = nameof(Skill);
         Field(_ => _.Id, type: typeof(IdGraphType));
         Field(_ => _.Name);
         Field<SkillType>("parent", resolve: context => repo.Get(context.Source.ParentId));
      }
   }
}
