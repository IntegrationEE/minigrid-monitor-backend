using Monitor.Common.Models;
using Monitor.Domain.Entities;

namespace Monitor.Business.Extensions
{
    public static class ChartConfigurationExtensions
    {
        public static ChartConfig Map(this ChartConfiguration config, int places = 2)
            => new ChartConfig
            {
                IsCumulative = config.IsCumulative,
                Convertable = config.Convertable,
                Places = places,
                Title = config.Title,
                Tooltip = config.Tooltip,
                UnitOfMeasure = config.UnitOfMeasure
            };
    }
}
