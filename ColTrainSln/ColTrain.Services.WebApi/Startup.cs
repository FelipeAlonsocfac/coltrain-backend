using ColTrain.Services.WebApi.Configurations;
using ColTrain.Shared.Infrastructure.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ColTrain.Services.WebApi
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
            services.AddDbContext<ColTrainDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlDataConnectionString")));
            services.AddApiVersioning();
            services.AddDependenceInjectionConfiguration();
            //var appConfiguration = Configuration.GetConfigurations();

            services.AddHttpContextAccessor();
            //services.SetCommonConfigurations(appConfiguration);

            services.AddHttpClient(); //to use httprequests
            services.AddCors(options => 
            {
                options.AddPolicy(
                    name: "AllowAngularApp",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1.0", new Info
                //{
                //    Title = "My APIs",
                //    Version = "v1.0",
                //    Description = "REST APIs "
                //});

                //**// Set the comments path for the Swagger JSON and UI.**
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/coltrain");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAngularApp");
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
