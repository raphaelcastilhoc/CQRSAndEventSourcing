using CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate;
using CQRSAndEventSourcing.Application.Commands;
using CQRSAndEventSourcing.Infrastructure.DAO;
using CQRSAndEventSourcing.Infrastructure.Factories;
using CQRSAndEventSourcing.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace CQRSAndEventSourcing.Api
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
            services.AddControllers();

            services
                .AddMediatR(typeof(AddEmployeeCommand).Assembly)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HumanResourcesApi", Version = "v1" });
                })
                .AddSingleton<IMongoDatabase>(x =>
                {
                    return new MongoDBFactory(Configuration).GetDatabase();
                })
                .AddScoped<IEventStore, EventStore>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HumanResourcesApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
