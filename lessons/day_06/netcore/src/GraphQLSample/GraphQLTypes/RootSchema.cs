using GraphQL;
using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Output;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootSchema : Schema
   {
      public RootSchema(IDependencyResolver resolver, RootQuery query, RootMutation mutation) : base(resolver)
      {
         Query = query;
         Mutation = mutation;

         RegisterType<ContactType>();
      }
   }
}
