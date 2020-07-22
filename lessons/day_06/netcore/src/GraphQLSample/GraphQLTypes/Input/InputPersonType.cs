using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Enums;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputPersonType : InputObjectGraphType<InputPerson>
   {
      public InputPersonType()
      {
         Name = nameof(InputPerson);
         Field(_ => _.Id, nullable: true, type: typeof(IdGraphType));
         Field(_ => _.Age, nullable: true);
         Field(_ => _.EyeColor, nullable: true, type: typeof(EyeColorType));
         Field(_ => _.FavSkill, nullable: true);
         Field(_ => _.TargetGrade, nullable: true, type: typeof(GradeType));
         Field(_ => _.TargetRole, nullable: true, type: typeof(RoleType));
         Field(_ => _.Grade, nullable: true, type: typeof(GradeType));
         Field(_ => _.Role, nullable: true, type: typeof(RoleType));
      }
   }
}
