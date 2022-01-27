using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskTracker_v2.Data;
using TaskTracker_v2.Repositories;
using TaskTracker_v2.Services;
using TaskTracker_v2.Validations;

namespace TaskTracker_v2
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

            // DbContext
            services.AddDbContext<TaskTrackerContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            // Controller
            services.AddControllers();
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskTracker_v2", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            // Automapper is used for wrapping everythin into Dtos
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Needed for JSON Patch
            services.AddControllersWithViews().AddNewtonsoftJson();
            // For Dependency Injection
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<ITaskRepo, TaskRepo>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskTrackerValidation, TaskTrackerValidation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTracker_v2 v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
