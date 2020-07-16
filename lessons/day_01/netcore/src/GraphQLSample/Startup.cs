using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLNetCore.Data;
using GraphQLNetCore.GraphQLTypes;
using GraphQLNetCore.Middleware;
using GraphQLNetCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace GraphQLNetCore
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GraphQLContext>(options => options.UseInMemoryDatabase(databaseName: "GraphQL"), ServiceLifetime.Transient);
           
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>(); //
            services.AddSingleton<IDocumentWriter, DocumentWriter>(); //

            // add something like repository
            services.AddSingleton<ISkillRepository, SkillRepository>();
            services.AddSingleton<RootQuery>();
            services.AddSingleton<SkillType>();
            // add schema
            services.AddSingleton<ISchema, RootSchema>();

            // add infrastructure stuff
            services.AddHttpContextAccessor();
            services.AddLogging(builder => builder.AddConsole());

            // add options configuration
            services.Configure<GraphQLSettings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<GraphQLMiddleware>();
            app.UseGraphQLPlayground();
        }
    }
}
