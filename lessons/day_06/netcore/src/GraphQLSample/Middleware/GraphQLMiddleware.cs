using System;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GraphQLNetCore.Middleware
{
    /// <summary>
    ///     Provides middleware for hosting GraphQL.
    /// </summary>
    public sealed class GraphQLMiddleware
    {
        private readonly string graphqlPath;
        private readonly RequestDelegate next;
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;
        private readonly IDocumentWriter writer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphQLMiddleware" /> class.
        /// </summary>
        /// <param name="next">
        ///     The next request delegate.
        /// </param>
        /// <param name="options">
        ///     The GraphQL options.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Throws <see cref="ArgumentNullException" /> if <paramref name="next" /> or <paramref name="options" /> is null.
        /// </exception>
        public GraphQLMiddleware(RequestDelegate next, IOptions<GraphQLOptions> options, ISchema _schema, IDocumentExecuter _executer, IDocumentWriter _writer)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (_schema == null)
            {
                throw new ArgumentException("Schema is null");
            }
            if (_executer == null)
            {
                throw new ArgumentException("Document Executer is null");
            }
            if (_writer == null)
            {
                throw new ArgumentException("Document Writer is null");
            }

            this.next = next;
            var optionsValue = options.Value;
            graphqlPath = string.IsNullOrEmpty(optionsValue?.GraphQLPath) ? GraphQLOptions.DefaultGraphQLPath : optionsValue.GraphQLPath;
            schema = _schema;
            executer = _executer;
            writer = _writer;
        }

        /// <summary>
        ///     Invokes the middleware with the specified context.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        /// <returns>
        ///     A <see cref="Task" /> representing the middleware invocation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throws <see cref="ArgumentNullException" /> if <paramref name="context" />.
        /// </exception>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (ShouldRespondToRequest(context.Request))
            {
                var executionResult = await ExecuteAsync(context.Request);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (executionResult.Errors?.Count ?? 0) == 0 ? 200 : 400;
                await writer.WriteAsync(context.Response.Body, executionResult);
            } else {
                await next(context);
            }
        }


        private async Task<ExecutionResult> ExecuteAsync(HttpRequest request)
        {
            var graphqlRequest = await JsonSerializer.DeserializeAsync<GraphQLRequest>
            (
                request.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return await executer.ExecuteAsync(option =>
            {
                option.Schema = schema;
                option.Query = graphqlRequest.Query;
                option.OperationName = graphqlRequest.OperationName;
            });
        }

        private bool ShouldRespondToRequest(HttpRequest request)
        {
            bool a = string.Equals(request.Method, "POST", StringComparison.OrdinalIgnoreCase);
            bool b = request.Path.Equals(graphqlPath);
            return a && b;
        }
    }
}
