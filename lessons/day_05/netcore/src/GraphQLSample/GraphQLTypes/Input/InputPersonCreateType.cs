using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputPersonCreateType : InputObjectGraphType<InputPersonCreate>
   {
      public InputPersonCreateType()
      {
         Name = "InputPersonCreate";
         Field(_ => _.name);
         Field(_ => _.surname, nullable: true);
         Field(_ => _.email, nullable: true);
         Field(_ => _.age, nullable: true);
         Field(_ => _.eyeColor, nullable: true, type: typeof(EyeColorType));
         Field(_ => _.friends, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.skills, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.favSkill, nullable: true);
      }
   }
}
