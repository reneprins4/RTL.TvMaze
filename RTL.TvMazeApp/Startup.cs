using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTL.TvMazeApp.Domain.Interfaces;
using RTL.TvMazeApp.Infrastructure.Contexts;
using RTL.TvMazeApp.Infrastructure.Repositories;
using RTL.TvMazeApp.Scraper.Handlers;
using RTL.TvMazeApp.Scraper.Services;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using AutoMapper;

namespace RTL.TvMazeApp
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
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<TvMazeContext>(o => o.UseSqlServer(
                Configuration.GetConnectionString("TvMaze"), optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
            
            services.AddScoped<DbContext, TvMazeContext>();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IScrapeService, ScrapeService>();
            services.AddTransient<IHostedService, ScrapeHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RTL TvMaze API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
