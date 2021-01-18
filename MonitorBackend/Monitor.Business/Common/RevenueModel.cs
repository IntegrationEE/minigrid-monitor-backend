namespace Monitor.Business.Common
{
    public class RevenueModel
    {
        public int Year { get; set; }
        public decimal Commercial { get; set; }
        public decimal Productive { get; set; }
        public decimal Residential { get; set; }
        public decimal Public { get; set; }

        public decimal Total => Commercial + Productive + Residential + Public;
    }
}
