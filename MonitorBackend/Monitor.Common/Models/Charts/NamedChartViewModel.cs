namespace Monitor.Common.Models
{
    public class NamedChartViewModel<TKey> : BaseChartPointViewModel<TKey>
    {
        public NamedChartViewModel(ChartConfig config)
            : base(config.IsCumulative, config.Places)
        { }

        public string Name { get; set; }
    }
}
