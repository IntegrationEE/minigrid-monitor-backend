using System;
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
    public class ProgrammeIndicatorValueService : BaseYearMonthIndicatorService<ProgrammeIndicatorValueViewModel, ProgrammeIndicatorValue, ProgrammeIndicatorValueQuartile>, IProgrammeIndicatorValueService
    {
        public ProgrammeIndicatorValueService(IBaseRepository repository)
            : base(repository)
        {
            ApplicableHeaders = new string[]
            {
                ChartConstans.VALUE,
            };
        }

        public override async Task<List<ProgrammeIndicatorValueViewModel>> GetAll(int indicatorId)
        {
            using (Repository)
            {
                return await Repository
                    .GetQuery<ProgrammeIndicatorValue>(entity => entity.ProgrammeIndicatorId == indicatorId)
                    .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                    .ProjectTo<ProgrammeIndicatorValueViewModel>(Repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public override async Task<ProgrammeIndicatorValueViewModel> Create(int indicatorId, ProgrammeIndicatorValueViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfDuplicated(indicatorId, model);

                var entity = CreateOrUpdateEntity(indicatorId, null, model);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<ProgrammeIndicatorValueViewModel> Update(int id, ProgrammeIndicatorValueViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<ProgrammeIndicatorValue>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        protected override ProgrammeIndicatorValue CreateOrUpdateEntity(int indicatorId, ProgrammeIndicatorValue entity, ProgrammeIndicatorValueViewModel model)
        {
            entity ??= new ProgrammeIndicatorValue(indicatorId, model.Year, model.Month);
            MapViewModel(model, entity);

            return entity;
        }

        protected override async Task<IList<ProgrammeIndicatorValue>> GetAllByYearAndMonth(int id, int[] years, int[] months)
            => await Repository
                .GetQuery<ProgrammeIndicatorValue>(z => z.ProgrammeIndicatorId == id && years.Contains(z.Year) && months.Contains(z.Month))
                .ToListAsync();

        protected override async Task<ProgrammeIndicatorValueQuartile> GetQuartiles(int indicatorId)
            => await Repository
                .InterpolateSql<ProgrammeIndicatorValueQuartile>($"SELECT * FROM getProgrammeIndicatorValueQuartiles({indicatorId})")
                .FirstOrDefaultAsync();

        protected override Outlier ValidateOutliersProperties(ProgrammeIndicatorValueQuartile quartile, ProgrammeIndicatorValueViewModel model)
        {
            var outliers = new Outlier(model.Year, model.Month);

            if (quartile.ValueMinMax.IsOutlier(model.Value))
            { outliers.AddProperty(nameof(model.Value)); }

            return outliers.Properties.Count > 0 ? outliers : null;
        }

        private void MapViewModel(ProgrammeIndicatorValueViewModel model, ProgrammeIndicatorValue entity)
        {
            entity.Set(model.Value);
        }

        private async Task CheckIfDuplicated(int parentId, ProgrammeIndicatorValueViewModel model)
        {
            if (await Repository.Exists<ProgrammeIndicatorValue>(z => z.ProgrammeIndicatorId == parentId && z.Year == model.Year && z.Month == model.Month))
            {
                throw new CustomException($"Exists Value for selected Year: '{model.Year}' and Month: '{model.Month}'.");
            }
        }
    }
}
