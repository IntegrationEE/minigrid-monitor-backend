using Monitor.Common.Models;

namespace Monitor.Common.Logic
{
    public interface IConverter
    {
        void Convert<Tkey>(BaseChartViewModel<Tkey> data);

        void Convert(ChartSeriesViewModel data);
    }
}
