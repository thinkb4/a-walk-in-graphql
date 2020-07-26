using GraphQL.Types;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputSkillType : InputObjectGraphType<InputSkill>
   {
      public InputSkillType()
      {
         Name = nameof(InputSkill);
         Field(_ => _.Id, nullable: true, type: typeof(IdGraphType));
         Field(_ => _.Name, nullable: true);
      }
   }
}
