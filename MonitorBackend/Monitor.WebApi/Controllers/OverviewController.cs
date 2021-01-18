using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Overview Controller
    /// </summary>
    public class OverviewController : AuthorizeController
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IGraphService _graphService;
        private readonly IProgrammeAnalyticsService _programmeAnalyticsService;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        /// <param name="graphService"></param>
        /// <param name="programmeAnalyticsService"></param>
        public OverviewController(IPortfolioService service,
            IGraphService graphService,
            IProgrammeAnalyticsService programmeAnalyticsService)
        {
            _portfolioService = service;
            _graphService = graphService;
            _programmeAnalyticsService = programmeAnalyticsService;
        }
        /// <summary>
        /// Get graph data
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns></returns>
        [HttpGet("{id}/sites")]
        public async Task<SiteGraphViewModel> GetGraph(int id)
            => await _graphService.Get(id);

        /// <summary>
        /// Get Programme's indicators charts
        /// </summary>
        /// <param name="id">Programme ID</param>
        /// <returns></returns>
        [HttpGet("{id}/programmes")]
        public async Task<List<TargetedChartViewModel>> GetProgrammeIndicators(int id)
            => await _programmeAnalyticsService.GetData(id);

        /// <summary>
        /// Get data for portfolio
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PortfolioViewModel> Portfolio([FromBody] FilterParametersViewModel filters)
            => await _portfolioService.GetData(filters);
    }
}
