using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace P79.Infrastructure.Excel
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddPdfInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            //services.AddTransient<IPdfService, PdfService>();
            return services;
        }
    }
}
