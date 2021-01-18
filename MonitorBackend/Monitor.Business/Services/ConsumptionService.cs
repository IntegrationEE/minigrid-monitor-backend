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
    public class ConsumptionService : BaseYearMonthIndicatorService<ConsumptionViewModel, Consumption, ConsumptionQuartile>, IConsumptionService
    {
        public ConsumptionService(IBaseRepository repository)
            : base(repository)
        {
            ApplicableHeaders = new string[]
            {
                ChartConstans.RESIDENTIAL,
                ChartConstans.COMMERCIAL,
                ChartConstans.PRODUCTIVE,
                ChartConstans.PUBLIC,
                ChartConstans.PEAK_LOAD,
                ChartConstans.TOTAL,
            };
        }

        public override async Task<List<ConsumptionViewModel>> GetAll(int siteId)
        {
            using (Repository)
            {
                return await Repository
                    .GetQuery<Consumption>(entity => entity.SiteId == siteId)
                    .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                    .ProjectTo<ConsumptionViewModel>(Repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public override async Task<ConsumptionViewModel> Create(int siteId, ConsumptionViewModel model)
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

        public override async Task<ConsumptionViewModel> Update(int id, ConsumptionViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<Consumption>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        protected override Outlier ValidateOutliersProperties(ConsumptionQuartile quartile, ConsumptionViewModel model)
        {
            var outliers = new Outlier(model.Year, model.Month);

            if (quartile.CommercialMinMax.IsOutlier(model.Commercial))
            { outliers.AddProperty(nameof(model.Commercial)); }

            if (quartile.PeakLoadMinMax.IsOutlier(model.PeakLoad))
            { outliers.AddProperty(nameof(model.PeakLoad)); }

            if (quartile.ProductiveMinMax.IsOutlier(model.Productive))
            { outliers.AddProperty(nameof(model.Productive)); }

            if (quartile.PublicMinMax.IsOutlier(model.Public))
            { outliers.AddProperty(nameof(model.Public)); }

            if (quartile.ResidentialMinMax.IsOutlier(model.Residential))
            { outliers.AddProperty(nameof(model.Residential)); }

            if (quartile.TotalMinMax.IsOutlier(model.Total))
            { outliers.AddProperty(nameof(model.Total)); }

            return outliers.Properties.Count > 0 ? outliers : null;
        }

        protected override async Task<IList<Consumption>> GetAllByYearAndMonth(int id, int[] years, int[] months)
            => await Repository
                .GetQuery<Consumption>(z => z.SiteId == id && years.Contains(z.Year) && months.Contains(z.Month))
                .ToListAsync();

        protected override async Task<ConsumptionQuartile> GetQuartiles(int siteId)
            => await Repository
                .InterpolateSql<ConsumptionQuartile>($"SELECT * FROM getConsumptionQuartiles({siteId})")
                .FirstOrDefaultAsync();

        protected override Consumption CreateOrUpdateEntity(int siteId, Consumption entity, ConsumptionViewModel model)
        {
            entity ??= new Consumption(siteId, model.Year, model.Month);
            MapViewModel(model, entity);

            return entity;
        }

        private void MapViewModel(ConsumptionViewModel model, Consumption entity)
        {
            entity.Set(model.Residential, model.Commercial, model.Productive, model.PeakLoad, model.Public, model.Total);
        }

        private async Task CheckIfDuplicated(int parentId, ConsumptionViewModel model)
        {
            if (await Repository.Exists<Consumption>(z => z.SiteId == parentId && z.Year == model.Year && z.Month == model.Month))
            {
                throw new CustomException($"Exists Consumption for selected Year: '{model.Year}' and Month: '{model.Month}'.");
            }
        }
    }
}
