using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;
using Wkhtmltopdf.NetCore.Options;

namespace HtmlToPdfApi.Services
{
    public interface IPdfService
    {
        Task<byte[]> GetPdf(string html, bool landscape = false);
    }
    public class PdfService : IPdfService
    {
        private readonly IGeneratePdf _generatePdf;

        public PdfService(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }
        public Task<byte[]> GetPdf(string html, bool landscape = false)
        {
            var options = new CustomOptions
            {
                PageOrientation = landscape ? Orientation.Landscape : Orientation.Portrait
            };

            _generatePdf.SetConvertOptions(options);

            return Task.FromResult(_generatePdf.GetPDF(html));
        }
    }
}
