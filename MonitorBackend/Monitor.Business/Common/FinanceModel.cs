namespace Monitor.Business.Common
{
    public class FinanceModel
    {
        public decimal Debt { get; set; }
        public decimal Equity { get; set; }
        public decimal Grant { get; set; }

        public decimal Total => Debt + Equity + Grant;
    }
}
