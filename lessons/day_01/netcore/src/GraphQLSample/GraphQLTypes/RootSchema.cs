using GraphQL;
using GraphQL.Types;
using System;

namespace GraphQLNetCore.GraphQLTypes
{
    public class RootSchema : Schema
    {
        private readonly IServiceProvider _services;
        public RootSchema(IServiceProvider services)
        {
            _services = services;
            Query = _services.GetService(typeof(RootQuery)).As<IObjectGraphType>();
        }
    }
}
