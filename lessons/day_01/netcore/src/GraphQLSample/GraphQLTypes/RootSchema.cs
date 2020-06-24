using GraphQL.Types;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootSchema : Schema
   {
      public RootSchema(RootQuery query)
      {
         Query = query;
      }
   }
}
