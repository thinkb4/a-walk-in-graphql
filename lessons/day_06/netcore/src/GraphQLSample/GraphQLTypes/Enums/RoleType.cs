using GraphQL.Types;
using GraphQLNetCore.Models.Enums;

namespace GraphQLNetCore.GraphQLTypes.Enums
{
   public class RoleType : EnumerationGraphType<Role>
   {
      public RoleType()
      {
         Name = nameof(Role);
      }
   }
}
