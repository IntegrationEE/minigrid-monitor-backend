using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.HostedServices;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Integrations Controller
    /// </summary>
    [ValidateUserRole(RoleCode.ADMINISTRATOR)]
    public class IntegrationsController : AuthorizeController
    {
        private readonly IIntegrationService _service;
        private readonly IIntegrationHostedService _hostedService;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        /// <param name="hostedService"></param>
        public IntegrationsController(IIntegrationService service, IIntegrationHostedService hostedService)
        {
            _service = service;
            _hostedService = hostedService;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<IntegrationLightModel>> GetByCurrent()
            => await _service.GetAll();

        /// <summary>
        /// Restart Background Service
        /// </summary>
        /// <returns></returns>
        [HttpGet("restart/{id}")]
        public void Restart(int id)
            => _hostedService.Restart(id, CurrentUserId);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IntegrationViewModel> Get(int id)
            => await _service.Get(id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IntegrationViewModel> Post([FromBody] IntegrationViewModel model)
        {
            var response = await _service.Create(model);

            ManageHostedService(response);

            return response;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IntegrationViewModel> Patch(int id, [FromBody] IntegrationViewModel model)
        {
            var response = await _service.Update(id, model);

            ManageHostedService(response);

            return response;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            ManageHostedService(new IntegrationViewModel { Id = id, TaskStatus = IntegrationTaskStatus.REMOVE });
            await _service.Delete(id);
        }

        private void ManageHostedService(IntegrationViewModel model)
        {
            if (!model.TaskStatus.HasValue)
            { return; }

            switch (model.TaskStatus.Value)
            {
                case IntegrationTaskStatus.ADD:
                    _hostedService.AddNew(model, CurrentUserId);
                    break;
                case IntegrationTaskStatus.RESTART:
                    _hostedService.Restart(model.Id, CurrentUserId);
                    break;
                case IntegrationTaskStatus.REMOVE:
                    _hostedService.Remove(model.Id, CurrentUserId);
                    break;
            }
        }
    }
}
