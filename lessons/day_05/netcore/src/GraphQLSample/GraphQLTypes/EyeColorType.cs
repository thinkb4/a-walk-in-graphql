using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class EyeColorType : EnumerationGraphType<EyeColor>
   {
      public EyeColorType()
      {
         Name = "EyeColor";
      }
   }
}
