using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Base CRUD Controller
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public abstract class BaseCRUDController<TViewModel, TService> : AuthorizeController
        where TViewModel : class, IBaseViewModel
        where TService : IBaseService<TViewModel>
    {
        /// <summary>
        /// Service
        /// </summary>
        protected readonly TService Service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        protected BaseCRUDController(TService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IList<TViewModel>> Get()
            => await Service.GetAll();

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TViewModel> Get(int id)
            => await Service.Get(id);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TViewModel> Post([FromBody] TViewModel model)
            => await Service.Create(model);

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
        /// Delete entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await Service.Delete(id);
        }
    }
}