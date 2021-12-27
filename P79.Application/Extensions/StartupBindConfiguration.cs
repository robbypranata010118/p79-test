using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P79.Domain.Settings;
using System.Collections.Generic;

namespace P79.Application.Extensions
{
    public static class StartupBindConfiguration
    {
        private const string APP_SETTINGS_JWT_BEARER_TOKEN_SECTION = "JwtBearerToken";
        private const string APP_SETTINGS_MODULES_SECTION = "Modules";
        private const string APP_SETTINGS_P79_SECTION = "P79";
        private const string APP_SETTINGS_ATTACHMEN_SECTION = "Attachment";
        private const string APP_SETTINGS_CORS_SECTION = "Cors";
        private const string APP_SETTINGS_ROLES_SECTION = "Roles";
        private const string APP_SETTINGS_BASE_SECTION = "Base";
        public static IServiceCollection BindConfigurationExtension(this IServiceCollection services, IConfiguration Configuration) {
            JWTSettings jwtSettings = new JWTSettings();
            List<Module> modules = new List<Module>();
            P79Old P79 = new P79Old();
            AttachmentSettings attachmentSettings = new AttachmentSettings();
            CorsSetting corsSetting = new CorsSetting();
            BaseSetting baseSetting = new BaseSetting();
            List<RoleSetting> roleSetting = new List<RoleSetting>();

            Configuration.Bind(APP_SETTINGS_MODULES_SECTION, modules);
            Configuration.Bind(APP_SETTINGS_JWT_BEARER_TOKEN_SECTION, jwtSettings);
            Configuration.Bind(APP_SETTINGS_P79_SECTION, P79);
            Configuration.Bind(APP_SETTINGS_ATTACHMEN_SECTION, attachmentSettings);
            Configuration.Bind(APP_SETTINGS_CORS_SECTION, corsSetting);
            Configuration.Bind(APP_SETTINGS_ROLES_SECTION, roleSetting);
            Configuration.Bind(APP_SETTINGS_BASE_SECTION, baseSetting);

            services.AddSingleton(jwtSettings);
            services.AddSingleton(modules);
            services.AddSingleton(P79);
            services.AddSingleton(attachmentSettings);
            services.AddSingleton(corsSetting);
            services.AddSingleton(roleSetting);
            services.AddSingleton(baseSetting);
            return services;
        }
    }
}
