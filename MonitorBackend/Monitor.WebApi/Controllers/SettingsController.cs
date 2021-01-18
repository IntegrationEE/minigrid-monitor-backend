using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Settings Controller
    /// </summary>
    public class SettingsController : AuthorizeController
    {
        private readonly ISettingService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public SettingsController(ISettingService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get All required codes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SettingViewModel>> GetAll()
            => await _service.GetAllForFrontend();
    }
}
