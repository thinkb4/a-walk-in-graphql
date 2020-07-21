using GraphQL.Types;
using GraphQLNetCore.Models.Enums;

namespace GraphQLNetCore.GraphQLTypes.Enums
{
   public class EyeColorType : EnumerationGraphType<EyeColor>
   {
      public EyeColorType()
      {
         Name = nameof(EyeColor);
      }
   }
}
