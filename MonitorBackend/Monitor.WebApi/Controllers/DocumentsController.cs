using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Documents Controller
    /// </summary>
    public class DocumentsController : AuthorizeController
    {
        private readonly IFileGeneratorService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public DocumentsController(IFileGeneratorService service)
        {
            _service = service;
        }
        /// <summary>
        /// Social
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("{type}/social/{format}")]
        public async Task<IActionResult> Social(SocialChartType type, FileFormat format, [FromBody] FilterParametersViewModel filters)
        {
            var results = await _service.GetSocial(filters, type, format);

            return GetFile(results, format);
        }
        /// <summary>
        /// Technical
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("{type}/technical/{format}")]
        public async Task<IActionResult> Technical(TechnicalChartType type, FileFormat format, [FromBody] FilterParametersViewModel filters)
        {
            var results = await _service.GetTechnical(filters, type, format);

            return GetFile(results, format);
        }
        /// <summary>
        /// Financial
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("{type}/financial/{format}")]
        public async Task<IActionResult> Financial(FinancialChartType type, FileFormat format, [FromBody] FilterParametersViewModel filters)
        {
            var results = await _service.GetFinancial(filters, type, format);

            return GetFile(results, format);
        }

        private IActionResult GetFile(byte[] results, FileFormat format)
        {
            if (results?.Length == 0)
                return BadRequest();

            return File(results, format == FileFormat.CSV ? Constants.CSV_CONTENT_TYPE : Constants.XLS_CONTENT_TYPE);
        }
    }
}
