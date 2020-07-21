using GraphQL.Types;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputSkillCreateType : InputObjectGraphType<InputSkillCreate>
   {
      public InputSkillCreateType()
      {
         Name = nameof(InputSkillCreate);
         Field(_ => _.Name);
         Field(_ => _.Parent, nullable: true, type: typeof(IdGraphType));
      }
   }
}
