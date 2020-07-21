using GraphQL.Types;
using GraphQLNetCore.Models.Abstractions;

namespace GraphQLNetCore.GraphQLTypes.Output
{
   public class EmployeeInterface : InterfaceGraphType<IEmployee>
   {
      public EmployeeInterface()
      {
         Name = nameof(IEmployee).Substring(1);
         Field(_ => _.EmployeeId);
      }
   }
}
