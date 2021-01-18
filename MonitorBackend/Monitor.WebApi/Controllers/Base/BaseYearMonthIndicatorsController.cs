using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Base Year and Month Indicators Controller
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public abstract class BaseYearMonthIndicatorsController<TViewModel, TService> : AuthorizeController
        where TViewModel : BaseYearMonthIndicatorModel
        where TService : IBaseYearMonthIndicatorService<TViewModel>
    {
        /// <summary>
        /// Service
        /// </summary>
        protected readonly TService Service;

        /// <summary>
        /// Ctr
        /// </summary>
        public BaseYearMonthIndicatorsController(TService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <param name="parentId">Parent Id (Site, Programme's Indicator)</param>
        /// <returns></returns>
        [HttpGet("{parentId}")]
        public virtual async Task<IList<TViewModel>> Get(int parentId)
            => await Service.GetAll(parentId);

        /// <summary>
        /// Export CSV/XLS
        /// </summary>
        /// <param name="parentId">Parent Id (Site, Programme's Indicator)</param>
        /// <param name="format"></param>
        /// <returns></returns>
        [HttpPost("{parentId}/export/{format}")]
        public virtual async Task<IActionResult> Export(int parentId, FileFormat format)
        {
            var models = await Service.GetAll(parentId);

            var results = Service.GenerateDocument(models, format);

            if (results?.Length == 0)
                return BadRequest();

            return File(results, format == FileFormat.CSV ? Constants.CSV_CONTENT_TYPE : Constants.XLS_CONTENT_TYPE);
        }

        /// <summary>
        /// Upload CSV/XLS
        /// </summary>
        /// <param name="parentId">Parent Id (Site, Programme's Indicator)</param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{parentId}/upload")]
        [DisableRequestSizeLimit]
        public virtual async Task<YearMonthIndicatorUploadResponse> Upload(int parentId, IFormFile file)
            => await Service.Upload(parentId, file);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="parentId">Parent Id (Site, Programme's Indicator)</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{parentId}")]
        public virtual async Task<TViewModel> Post(int parentId, [FromBody] TViewModel model)
            => await Service.Create(parentId, model);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public virtual async Task<TViewModel> Patch(int id, [FromBody] TViewModel model)
            => await Service.Update(id, model);

        /// <summary>
        /// Validate outliers
        /// </summary>
        /// <param name="parentId">Parent Id (Site, Programme's Indicator)</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{parentId}/validate")]
        public virtual async Task<YearMonthIndicatorValidateResponse> ValidateOutliers(int parentId, [FromBody] TViewModel model)
            => await Service.ValidateOutliers(parentId, model);
    }
}
