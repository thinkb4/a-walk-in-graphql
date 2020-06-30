using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GraphQLNetCore.Middleware
{
    public class GraphQLRequest
    {
        public string OperationName { get; set; }

        public string Query { get; set; }

    }
}
