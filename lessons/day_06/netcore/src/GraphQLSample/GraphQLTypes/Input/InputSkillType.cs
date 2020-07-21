using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputSkillType : InputObjectGraphType<InputSkill>
   {
      public InputSkillType()
      {
         Name = "InputSkill";
         Field(_ => _.id, nullable: true, type: typeof(IdGraphType));
         Field(_ => _.name, nullable: true);
      }
   }
}
