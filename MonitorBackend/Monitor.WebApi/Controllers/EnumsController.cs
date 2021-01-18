using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Enums Controller
    /// </summary>
    public class EnumsController : AuthorizeController
    {
        private readonly IEnumService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public EnumsController(IEnumService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get User Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        public IList<EnumViewModel<RoleCode>> GetUserRoles()
            => _service.GetList<RoleCode>();

        /// <summary>
        /// Get Renewable Technologies
        /// </summary>
        /// <returns></returns>
        [HttpGet("renewable-technologies")]
        public IList<EnumViewModel<RenewableTechnology>> GetRenewableTechnologies()
            => _service.GetList<RenewableTechnology>();

        /// <summary>
        /// Get Storage Technologies
        /// </summary>
        /// <returns></returns>
        [HttpGet("storage-technologies")]
        public IList<EnumViewModel<StorageTechnology>> GetStorageTechnologies()
            => _service.GetList<StorageTechnology>();

        /// <summary>
        /// Get Conventional Technologies
        /// </summary>
        /// <returns></returns>
        [HttpGet("conventional-technologies")]
        public IList<EnumViewModel<ConventionalTechnology>> GetConventionalTechnologies()
            => _service.GetList<ConventionalTechnology>();

        /// <summary>
        /// Get Grid Connections
        /// </summary>
        /// <returns></returns>
        [HttpGet("grid-connections")]
        public IList<EnumViewModel<GridConnectionType>> GetGridConnections()
            => _service.GetList<GridConnectionType>();

        /// <summary>
        /// Get Convertable types
        /// </summary>
        /// <returns></returns>
        [HttpGet("convertable-types")]
        public IList<EnumViewModel<ConvertableType>> GetConvertableTypes()
            => _service.GetList<ConvertableType>();
    }
}