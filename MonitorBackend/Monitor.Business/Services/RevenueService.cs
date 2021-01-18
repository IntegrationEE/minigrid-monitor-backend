using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Common;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.Quartiles;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class RevenueService : BaseYearMonthIndicatorService<RevenueViewModel, Revenue, RevenueQuartile>, IRevenueService
    {
        public RevenueService(IBaseRepository repository)
            : base(repository)
        {
            ApplicableHeaders = new string[]
            {
                ChartConstans.RESIDENTIAL,
                ChartConstans.COMMERCIAL,
                ChartConstans.PRODUCTIVE,
                ChartConstans.PUBLIC,
            };
        }

        public override async Task<List<RevenueViewModel>> GetAll(int siteId)
        {
            using (Repository)
            {
                return await Repository
                    .GetQuery<Revenue>(entity => entity.SiteId == siteId)
                    .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                    .ProjectTo<RevenueViewModel>(Repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public override async Task<RevenueViewModel> Create(int siteId, RevenueViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfDuplicated(siteId, model);

                var entity = CreateOrUpdateEntity(siteId, null, model);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<RevenueViewModel> Update(int id, RevenueViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<Revenue>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        protected override Outlier ValidateOutliersProperties(RevenueQuartile quartile, RevenueViewModel model)
        {
            var outliers = new Outlier(model.Year, model.Month);

            if (quartile.CommercialMinMax.IsOutlier(model.Commercial))
            { outliers.AddProperty(nameof(model.Commercial)); }

            if (quartile.ProductiveMinMax.IsOutlier(model.Productive))
            { outliers.AddProperty(nameof(model.Productive)); }

            if (quartile.PublicMinMax.IsOutlier(model.Public))
            { outliers.AddProperty(nameof(model.Public)); }

            if (quartile.ResidentialMinMax.IsOutlier(model.Residential))
            { outliers.AddProperty(nameof(model.Residential)); }

            return outliers.Properties.Count > 0 ? outliers : null;
        }

        protected override async Task<IList<Revenue>> GetAllByYearAndMonth(int id, int[] years, int[] months)
            => await Repository
                .GetQuery<Revenue>(z => z.SiteId == id && years.Contains(z.Year) && months.Contains(z.Month))
                .ToListAsync();

        protected override async Task<RevenueQuartile> GetQuartiles(int siteId)
            => await Repository
                .InterpolateSql<RevenueQuartile>($"SELECT * FROM getRevenueQuartiles({siteId})")
                .FirstOrDefaultAsync();

        protected override Revenue CreateOrUpdateEntity(int siteId, Revenue entity, RevenueViewModel model)
        {
            entity ??= new Revenue(siteId, model.Year, model.Month);
            MapViewModel(model, entity);

            return entity;
        }

        private void MapViewModel(RevenueViewModel model, Revenue entity)
        {
            entity.Set(model.Residential, model.Commercial, model.Productive, model.Public);
        }

        private async Task CheckIfDuplicated(int parentId, RevenueViewModel model)
        {
            if (await Repository.Exists<Revenue>(z => z.SiteId == parentId && z.Year == model.Year && z.Month == model.Month))
            {
                throw new CustomException($"Exists Revenue for selected Year: '{model.Year}' and Month: '{model.Month}'.");
            }
        }
    }
}
