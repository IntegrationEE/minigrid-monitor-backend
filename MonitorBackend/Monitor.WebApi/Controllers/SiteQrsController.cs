using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Site QRs Controller
    /// </summary>
    [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    public class SiteQrsController : AuthorizeController
    {
        private readonly ISiteQrService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public SiteQrsController(ISiteQrService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Qr Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.Get(id);

            if (result?.Length == 0)
                return BadRequest();

            return File(result, Constants.IMAGE_PNG_CONTENT_TYPE);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [CanManageSite("id", Order = 2)]
        public async Task Update(int id)
            => await _service.Update(id);
    }
}
