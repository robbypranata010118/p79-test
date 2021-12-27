using System;
using System.Threading.Tasks;
using P79.Base.Interfaces;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace P79.Infrastructure.Pdf.Services
{
    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public async Task<byte[]> GeneratePdf(string data)
        {
            try
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A5,
                        DocumentTitle = "PDF Report",
                        Out = ""
                    },
                    Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HtmlContent = data,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9 , Right = ""},
                        FooterSettings = { FontSize = 9 , Right = ""}
                    }
                }
                };
                return await Task.FromResult(_converter.Convert(doc));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<byte[]> TestPdf()
        {
            try
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A5,
                        DocumentTitle = "PDF Report",
                        Out = ""
                    },
                    Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HtmlContent = "<p> hi im generated pdf</p>",
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9 , Right = ""},
                        FooterSettings = { FontSize = 9 , Right = ""}
                    }
                }
                };
                return await Task.FromResult(_converter.Convert(doc));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
