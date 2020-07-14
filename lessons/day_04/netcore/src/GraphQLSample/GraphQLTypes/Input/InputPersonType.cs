using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputPersonType : InputObjectGraphType<InputPerson>
   {
      public InputPersonType()
      {
         Field(_ => _.id, nullable: true);
         Field(_ => _.age, nullable: true);
         Field(_ => _.eyeColor, nullable: true, type: typeof(EyeColorType));
         Field(_ => _.favSkill, nullable: true);
      }
   }
}
