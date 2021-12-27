using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P79.Base.Interfaces;
using P79.Infrastructure.Pdf.Services;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace P79.Infrastructure.Pdf
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddPdfInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IPdfService, PdfService>();
            return services;
        }
    }
}
