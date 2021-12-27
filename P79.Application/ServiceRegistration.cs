using P79.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using P79.Base.Interfaces;
using Microsoft.Extensions.Configuration;
using P79.Infrastructure.Pdf;
using System.Globalization;

namespace P79.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationExtension(this IServiceCollection services, IConfiguration configuration) {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("id");
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddPdfInfrastucture(configuration);
        }
    }
}
