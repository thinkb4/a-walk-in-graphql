using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Enums;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputCandidateCreateType : InputObjectGraphType<InputCandidateCreate>
   {
      public InputCandidateCreateType()
      {
         Name = nameof(InputCandidateCreate);
         Field(_ => _.Name);
         Field(_ => _.Surname);
         Field(_ => _.Email);
         Field(_ => _.Age);
         Field(_ => _.TargetGrade, type: typeof(NonNullGraphType<GradeType>));
         Field(_ => _.TargetRole, type: typeof(NonNullGraphType<RoleType>));
         Field(_ => _.EyeColor, nullable: true, type: typeof(EyeColorType));
         Field(_ => _.Friends, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.Skills, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.FavSkill, nullable: true, type: typeof(IdGraphType));
      }
   }
}
