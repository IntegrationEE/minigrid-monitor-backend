using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Business.Services;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// States Controller
    /// </summary>
    public class StatesController : AuthorizeController
    {
        private readonly IStateService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public StatesController(IStateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<BaseLightModel>> GetAll()
            => await _service.GetAll();

        /// <summary>
        /// Get All for map
        /// </summary>
        /// <returns></returns>
        [HttpGet("map")]
        public async Task<IList<StateMapModel>> GetAllForMap()
            => await _service.GetListForMap();

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        [HttpGet("filters")]
        public async Task<IList<FilterLightModel>> GetList()
            => await _service.GetListForFilters(CurrentUser);
    }
}
