using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class ChartConfig
    {
        public ChartConfig()
        {
            Places = 2;
            MinCount = 1;
        }

        public int Places { get; set; }

        public int MinCount { get; set; }

        public bool IsCumulative { get; set; }

        public ConvertableType? Convertable { get; set; }

        public string Title { get; set; }

        public string Tooltip { get; set; }

        public string UnitOfMeasure { get; set; }
    }
}
