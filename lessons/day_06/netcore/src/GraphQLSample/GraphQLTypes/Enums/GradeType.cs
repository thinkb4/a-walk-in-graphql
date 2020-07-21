using GraphQL.Types;
using GraphQLNetCore.Models.Enums;

namespace GraphQLNetCore.GraphQLTypes.Enums
{
   public class GradeType : EnumerationGraphType<Grade>
   {
      public GradeType()
      {
         Name = nameof(Grade);
      }
   }
}
