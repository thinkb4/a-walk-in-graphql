using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraphQLNetCore.Controllers
{
    [ApiController]
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer; private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQueryDto query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToInputs();
                
            }).ConfigureAwait(false);

            if(result.Errors?.Count > 0)
            {
                return Problem(detail: result.Errors.Select(_ => _.Message).FirstOrDefault(), statusCode:500);
            }
            return Ok(result.Data);
        }

        [HttpGet]
        public string Get()
        {
            return string.Empty;
        }
    }
}
