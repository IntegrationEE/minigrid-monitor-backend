using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class ProgrammeService : BaseService<ProgrammeViewModel, Programme>, IProgrammeService
    {
        public ProgrammeService(IBaseRepository repository)
            : base(repository)
        { }

        public async Task<IList<ProgrammeViewModel>> GetByCurrent(UserInfo currentUser)
        {
            using (Repository)
            {
                Expression<Func<Programme, bool>> exp = null;

                if (currentUser.Role == RoleCode.PROGRAMME_MANAGER)
                {
                    exp = x => x.Users.Any(z => z.UserId == currentUser.Id);
                }

                return await Repository.GetListWithOrder<ProgrammeViewModel, Programme, string>(exp, x => x.Name);
            }
        }

        public async Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser)
        {
            using (Repository)
            {
                Expression<Func<Programme, bool>> programmeExpression = null;
                Expression<Func<Site, bool>> siteExpression = null;

                switch (currentUser.Role)
                {
                    case RoleCode.DEVELOPER:
                        programmeExpression = x => x.Sites.Any(z => z.CompanyId == currentUser.CompanyId);
                        siteExpression = x => x.CompanyId == currentUser.CompanyId;
                        break;
                    case RoleCode.PROGRAMME_MANAGER:
                        programmeExpression = x => x.Users.Any(z => z.UserId == currentUser.Id);
                        siteExpression = x => true;
                        break;
                    default:
                        programmeExpression = x => true;
                        siteExpression = x => true;
                        break;
                }

                return await Repository.GetQuery<Programme>()
                    .Where(programmeExpression)
                    .Select(x => new FilterLightModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SiteIds = x.Sites.AsQueryable().Where(siteExpression).Select(z => z.Id)
                    })
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
        }

        public override async Task<ProgrammeViewModel> Create(ProgrammeViewModel model)
        {
            using (Repository)
            {
                var entity = new Programme();
                MapViewModel(model, entity);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<ProgrammeViewModel> Update(int id, ProgrammeViewModel model)
        {
            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<Programme>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        private void MapViewModel(ProgrammeViewModel model, Programme entity)
        {
            entity.Set(model.Name);
        }
    }
}
