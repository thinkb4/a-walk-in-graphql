using GraphQL.Types;
using GraphQLNetCore.Models.Input;

namespace GraphQLNetCore.GraphQLTypes
{
   public class InputGlobalSearchType : InputObjectGraphType<InputGlobalSearch>
   {
      public InputGlobalSearchType()
      {
         Name = nameof(InputGlobalSearch);
         Field(_ => _.Name, type: typeof(NonNullGraphType<StringGraphType>));
      }
   }
}
