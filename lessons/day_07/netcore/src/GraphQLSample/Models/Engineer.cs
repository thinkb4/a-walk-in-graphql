using GraphQLNetCore.Models.Abstractions;
using GraphQLNetCore.Models.Enums;

namespace GraphQLNetCore.Models
{
   public class Engineer: Person, IEmployee
   {
      public int EmployeeId { get; set; }
      public Role Role { get; set; }
      public Grade Grade { get; set; }
   }
}
