using HtmlToPdfApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HtmlToPdfApi.Controllers
{
    [ApiVersion(ApiVersions.V1)]
    [Route(ApiRouteTemplate.ROUTE_ENTITY)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiExplorerSettings(GroupName = "Pdf")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;
        private readonly IPdfService _pdfService;

        public PdfController(ILogger<PdfController> logger, IPdfService pdfService)
        {
            _logger = logger;
            _pdfService = pdfService;
        }

        /// <summary>
        /// Gets the PDF.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="landscape">if set to <c>true</c> [landscape]. otherwise [portrait]</param>
        /// <param name="fileName">Name of the file. {0} will be replaced with UTC 8601 DateTime</param>
        /// <response code="200">File result</response>
        /// <response code="500">Server error processing pdf</response>
        /// <returns>Pdf file as <see cref="T:byte[]"/></returns>
        [HttpPut("GetPdf")]
        [Consumes(MediaTypeNames.Text.Html)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPdf([FromBody] string html, [FromQuery] bool landscape = false, [FromQuery] string fileName = "File_{0}.pdf")
        {
            _logger.LogInformation($"Incoming Pdf Creation request for: {fileName}");

            var pdf = await _pdfService.GetPdf(html, landscape);

            _logger.LogInformation($"Pdf generated: {fileName}");

            return File(pdf, MediaTypeNames.Application.Pdf, string.Format(fileName, DateTimeOffset.UtcNow.ToString("o")));
        }
    }
}
