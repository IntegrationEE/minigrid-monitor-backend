using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class CompanyService : BaseService<CompanyViewModel, Company>, ICompanyService
    {
        public CompanyService(IBaseRepository repository)
            : base(repository)
        { }

        public async Task<IList<BaseLightModel>> GetList()
        {
            using (Repository)
            {
                return await Repository.GetListWithOrder<BaseLightModel, Company, object>(null, x => x.Name);
            }
        }

        public async Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser)
        {
            using (Repository)
            {
                Expression<Func<Company, bool>> exp = null;

                if (currentUser.Role == RoleCode.DEVELOPER)
                {
                    exp = x => x.Id == currentUser.CompanyId;
                }

                return await Repository.GetListWithOrder<FilterLightModel, Company, string>(exp, x => x.Name);
            }
        }

        public override async Task<CompanyViewModel> Create(CompanyViewModel model)
        {
            using (Repository)
            {
                await model.IsValid();

                var entity = new Company();
                MapViewModel(model, entity);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<CompanyViewModel> Update(int id, CompanyViewModel model)
        {
            using (Repository)
            {
                await model.IsValid();
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<Company>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        private void MapViewModel(CompanyViewModel model, Company entity)
        {
            entity.Set(model.Name, model.WebsiteUrl, model.PhoneNumber);
            entity.SetAddress(model.City, model.Street, model.Number, model.StateId, model.LocalGovernmentAreaId);
        }
    }
}
