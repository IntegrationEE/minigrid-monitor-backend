namespace Monitor.Business.Common
{
    public class AverageConsumptionModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public decimal Productive { get; set; }
        public decimal Residential { get; set; }
        public decimal Commercial { get; set; }
        public decimal Public { get; set; }

        public decimal Total => Productive + Residential + Commercial + Public;
    }
}
