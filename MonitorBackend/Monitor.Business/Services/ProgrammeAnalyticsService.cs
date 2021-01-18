using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;

namespace Monitor.Business.Services
{
    public class ProgrammeAnalyticsService : IProgrammeAnalyticsService
    {
        private readonly IBaseRepository _repository;

        public ProgrammeAnalyticsService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TargetedChartViewModel>> GetData(int programmeId)
        {
            var response = new List<TargetedChartViewModel>();

            using (_repository)
            {
                var chartGenerator = new ChartGenerator();

                var indicators = await _repository.GetQuery<ProgrammeIndicator>(z => z.ProgrammeId == programmeId && z.IsEnabled)
                    .Select(z => new
                    {
                        z.Id,
                        z.IsCumulative,
                        z.Name,
                        z.Description,
                        z.Target,
                        z.Unit
                    }).ToListAsync();

                var values = await _repository.GetQuery<ProgrammeIndicatorValue>(z => indicators.Select(x => x.Id).Contains(z.ProgrammeIndicatorId))
                    .Where(z => z.Value.HasValue)
                    .Select(x => new
                    {
                        x.Value.Value,
                        x.Year,
                        x.ProgrammeIndicatorId,
                    })
                    .GroupBy(z => new { z.ProgrammeIndicatorId, z.Year })
                    .Select(z => new Tuple<int, int, decimal>(
                        z.Key.ProgrammeIndicatorId,
                        z.Key.Year,
                        z.Sum(x => x.Value)
                    )).ToListAsync();

                foreach (var indicator in indicators)
                {
                    var indicatorValues = values
                        .Where(z => z.Item1 == indicator.Id)
                        .Select(z => (Year: z.Item2, Value: z.Item3));

                    var chartConfig = new ChartConfig
                    {
                        IsCumulative = indicator.IsCumulative,
                        UnitOfMeasure = indicator.Unit,
                        Title = indicator.Name,
                    };

                    response.Add(new TargetedChartViewModel(chartConfig)
                    {
                        Description = indicator.Description,
                        Target = indicator.Target,
                        TemporaryPoints = chartGenerator.GetSeries(indicatorValues, chartConfig)
                    });
                }
            }

            return response;
        }
    }
}
