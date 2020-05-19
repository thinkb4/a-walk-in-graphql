using GraphQL;
using GraphQL.Types;

namespace GraphQLNetCore.GraphQLTypes
{
    public class RootSchema:Schema, ISchema
    {
        public RootSchema(IDependencyResolver resolver):base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
        }
    }
}
