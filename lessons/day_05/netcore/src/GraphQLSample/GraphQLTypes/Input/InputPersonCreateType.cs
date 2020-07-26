using GraphQL.Types;
using GraphQLNetCore.Models;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputPersonCreateType : InputObjectGraphType<InputPersonCreate>
   {
      public InputPersonCreateType()
      {
         Name = nameof(InputPersonCreate);
         Field(_ => _.Name);
         Field(_ => _.Surname, nullable: true);
         Field(_ => _.Email, nullable: true);
         Field(_ => _.Age, nullable: true);
         Field(_ => _.EyeColor, nullable: true, type: typeof(EyeColorType));
         Field(_ => _.Friends, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.Skills, nullable: true, type: typeof(ListGraphType<NonNullGraphType<IdGraphType>>));
         Field(_ => _.FavSkill, nullable: true);
      }
   }
}
