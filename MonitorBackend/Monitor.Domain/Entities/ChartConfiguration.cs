using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class ChartConfiguration : BaseEntity
    {
        public ChartConfiguration(ChartCode code, ChartType type)
        {
            Code = code;
            Type = type;
        }

        public ChartCode Code { get; private set; }
        public ChartType Type { get; private set; }

        [Required]
        public string Title { get; private set; }
        public string Tooltip { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public bool IsCumulative { get; private set; }
        public ConvertableType? Convertable { get; private set; }

        public void Set(string title, string tooltip, string unitOfMeasure, bool isCumulative, ConvertableType? convertable)
        {
            Title = title;
            Tooltip = tooltip;
            UnitOfMeasure = unitOfMeasure;
            IsCumulative = isCumulative;
            Convertable = convertable;
        }
    }
}
