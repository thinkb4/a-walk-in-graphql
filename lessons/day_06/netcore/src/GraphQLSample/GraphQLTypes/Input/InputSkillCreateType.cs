using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputSkillCreateType : InputObjectGraphType<InputSkillCreate>
   {
      public InputSkillCreateType()
      {
         Name = "InputSkillCreate";
         Field(_ => _.name);
         Field(_ => _.parent, nullable: true, type: typeof(IdGraphType));
      }
   }
}
