using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputSkillType : InputObjectGraphType<InputSkill>
   {
      public InputSkillType()
      {
         Field(_ => _.id, nullable: true);
         Field(_ => _.name, nullable: true);
      }
   }
}
