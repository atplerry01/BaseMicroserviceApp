using API.Middleware.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddCustomAPIVersioning(this IServiceCollection services)
        {

            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();

                // opt.ApiVersionReader = ApiVersionReader.Combine(
                //  new HeaderApiVersionReader("X-Version"),
                //   new QueryStringApiVersionReader("ver", "version"));

                //opt.Conventions.Controller<TalksController>()
                //  .HasApiVersion(new ApiVersion(1, 0))
                //  .HasApiVersion(new ApiVersion(1, 1))
                //  .Action(c => c.Delete(default(string), default(int)))
                //    .MapToApiVersion(1, 1);

            });


            services.AddVersionedApiExplorer(
               options =>
               {
                   // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                   // note: the specified format code will format the version as "'v'major[.minor][-status]"
                   options.GroupNameFormat = "'v'VVV";

                   // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                   // can also be used to control the format of the API version in route templates
                   options.SubstituteApiVersionInUrl = true;
               });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath.getXmlCommentsFilePath());
                });

            return services;
        }

    }
}
