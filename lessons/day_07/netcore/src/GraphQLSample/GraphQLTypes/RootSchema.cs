using System;
using GraphQL.Types;
using GraphQLNetCore.GraphQLTypes.Output;

namespace GraphQLNetCore.GraphQLTypes
{
   public class RootSchema : Schema
   {
      public RootSchema(IServiceProvider service, RootQuery query, RootMutation mutation) : base(service)
      {
         Query = query;
         Mutation = mutation;

         RegisterType<ContactType>();
      }
   }
}
