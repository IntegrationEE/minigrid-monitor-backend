using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Advanced Analytics Controller
    /// </summary>
    public class AdvancedAnalyticsController : AuthorizeController
    {
        private readonly IAdvancedTechnicalService _technicalService;
        private readonly IAdvancedSocialService _socialService;
        private readonly IAdvancedFinancialService _financialService;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="technicalService"></param>
        /// <param name="socialService"></param>
        /// <param name="financialService"></param>
        public AdvancedAnalyticsController(IAdvancedTechnicalService technicalService,
            IAdvancedSocialService socialService,
            IAdvancedFinancialService financialService)
        {
            _technicalService = technicalService;
            _socialService = socialService;
            _financialService = financialService;
        }
        /// <summary>
        /// Social
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("social")]
        public async Task<SocialViewModel> Social([FromBody] FilterParametersViewModel filters)
            => await _socialService.GetCharts(filters);
        /// <summary>
        /// Technical
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("technical")]
        public async Task<TechnicalViewModel> Technical([FromBody] FilterParametersViewModel filters)
            => await _technicalService.GetCharts(filters);
        /// <summary>
        /// Financial
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("financial")]
        public async Task<FinancialViewModel> Financial([FromBody] FilterParametersViewModel filters)
            => await _financialService.GetCharts(filters);
    }
}