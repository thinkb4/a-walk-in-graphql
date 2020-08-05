using GraphQLNetCore.Models.Enums;

namespace GraphQLNetCore.Models
{
   public class Candidate: Person
   {
      public Role TargetRole { get; set; }
      public Grade TargetGrade { get; set; }
   }
}
