namespace Monitor.Business.Common
{
    public class CapexModel
    {
        public decimal Generation { get; set; }
        public decimal SiteDevelopment { get; set; }
        public decimal Logistics { get; set; }
        public decimal Distributions { get; set; }
        public decimal Commissioning { get; set; }
        public decimal Taxes { get; set; }
        public decimal CustomerInstallation { get; set; }

        public decimal Total => Generation + SiteDevelopment + Logistics + Distributions + Commissioning + Taxes + CustomerInstallation;
    }
}
