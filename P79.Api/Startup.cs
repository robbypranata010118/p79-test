using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using P79.Api.Admin.Extensions;
using P79.Api.Admin.Helpers;
using P79.Application;
using P79.Application.Extensions;
using P79.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using Microsoft.Extensions.Logging;
using P79.Api.Admin.Middlewares;
namespace P79.Api.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Hour)
                     .CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
#pragma warning disable CS1591
        public void ConfigureServices(IServiceCollection services)
        {

            services.BindConfigurationExtension(Configuration);
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddNewtonsoftJson(options =>
            {
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services
               .Configure<ApiBehaviorOptions>(options =>
               {
                   // disable the automatic model state validation before reaching controller (remove overriding)
                   options.SuppressModelStateInvalidFilter = true;
               })
               .Configure<RouteOptions>(options =>
               {
                   // this will make url path to lower case
                   options.LowercaseUrls = true;
               });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            services.AddApiVersioningExtension(Configuration);
            services.AddApplicationExtension(Configuration);
            services.AddSwaggerExtension(Configuration);
            services.AddJWTExtension(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddHttpContextAccessor();
            //services.AddOwnCorsConfiguration(Configuration);
            services.AddSignalR();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("DefaultPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension(env, provider);
            app.UseExceptionMiddleware();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
