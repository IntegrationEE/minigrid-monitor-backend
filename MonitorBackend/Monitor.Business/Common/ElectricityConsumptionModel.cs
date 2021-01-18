namespace Monitor.Business.Common
{
    public class ElectricityConsumptionModel
    {
        public int Year { get; set; }
        public decimal Productive { get; set; }
        public decimal Commercial { get; set; }
        public decimal Residential { get; set; }
        public decimal Public { get; set; }

        public decimal Total => Productive + Commercial + Residential + Public;
    }
}
