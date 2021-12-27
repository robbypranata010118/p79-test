using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using P79.Api.Admin.Filters;

namespace P79.Api.Admin.Extensions
{
    public static class StartupSwaggerExtension
    {
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                c.OperationFilter<SwaggerDefaultValues>();
                //c.OperationFilter<RemoveQueryApiVersionParamOperationFilter>();
                //c.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), XmlCommentsFileName));
                //c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.EnableAnnotations();
                c.SchemaFilter<SwaggerAddEnumDescriptions>();
                var providers = services.BuildServiceProvider();
                var service = providers.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in service.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName,CreateMetaInfoAPIVersion(description));
                }
                c.ExampleFilters();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            return services;
        }
        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider) {
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                });
            }
            return app;
        }
        #region Private
        private static OpenApiInfo CreateMetaInfoAPIVersion(ApiVersionDescription apiDescription)
        {
            var info = new OpenApiInfo()
            {
                Title = "P79 - Next Generation API Admin",
                Version = apiDescription.ApiVersion.ToString()
            };

            if (apiDescription.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;

        }

        static string XmlCommentsFileName
        {
            get
            {
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return fileName;
            }
        }

        #endregion
    }
}
