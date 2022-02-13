using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Salaries.API.Infrastructure;
using Salaries.API.Infrastructure.Factories;
using Salaries.Domain.AggregatesModel.SalaryAggregate;
using Salaries.Infrastructure;
using Salaries.Infrastructure.Repositories;

namespace Salaries.API
{
    public class Startup
    {
        private readonly string _CorsPolicy = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Salaries.API", Version = "v1" });
            });

            var serviceProvider = services.BuildServiceProvider();

            var env = serviceProvider.GetService<IWebHostEnvironment>();
            var settings = serviceProvider.GetService<IOptions<SalariesSettings>>();
            var logger = serviceProvider.GetService<ILogger<SalariesContextSeed>>();

            // Create the database
            SalariesDbContextFactory contextFactory = new SalariesDbContextFactory();
            SalariesContext context = contextFactory.CreateDbContext(Array.Empty<string>());
            new SalariesContextSeed()
                .SeedAsync(context, env, logger)
                .Wait();

            // Inject the DB Context and repository
            services.AddDbContext<SalariesContext>(c => c.UseSqlServer(Configuration["ConnectionString"]),
                ServiceLifetime.Scoped);
            services.AddScoped<ISalaryRepository, SalaryRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _CorsPolicy, builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "http://localhost:4200/")
                    //    .AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Salaries.API v1"));
            }

            app.UseCors(_CorsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
