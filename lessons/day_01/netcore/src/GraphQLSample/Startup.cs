using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLNetCore.Data;
using GraphQLNetCore.GraphQLTypes;
using GraphQLNetCore.Models;
using GraphQLNetCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
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
            services.AddDbContext<GraphQLContext>(options => options.UseInMemoryDatabase(databaseName: "GraphQL"));
            services.AddScoped<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISchema, RootSchema>();
            services.AddScoped<RootQuery>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<SkillType>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
            app.UseRouting().UseEndpoints(
                routing => routing.MapControllers()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
