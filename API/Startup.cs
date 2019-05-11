using API.Extensions;
using LoggerClassLib.Filters;
using LoggerClassLib.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

[assembly: ApiConventionType(typeof(DefaultApiConventions))] // automatically add standard endpoints attributes
namespace API
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

            //services.AddAutoMapper();

            services.AddCustomAPIVersioning();
            services.AddCustomSwagger();


            services.AddMvc(options =>
            {
                options.Filters.Add(new TrackPerformanceFilter()); // performance tracking logger middleware coming from LoggerClassLib

            })
            .AddJsonOptions(options => // for Swagger JSON indentation
                        {
                options.SerializerSettings.Formatting = Formatting.Indented;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.AddCustomSwagger(provider);
            

            app.UseHttpsRedirection();
            app.UseApiExceptionHandler();  // from custom helper assembly for logging Errors on application level coming from LoggerClassLib
            app.UseMvc();
        }


    }
}
