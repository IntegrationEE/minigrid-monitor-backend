namespace Monitor.Business.Common
{
    public class InstalledCapacityModel
    {
        public int Year { get; set; }
        public decimal PV { get; set; }
        public decimal Hydro { get; set; }
        public decimal Biomass { get; set; }
        public decimal Wind { get; set; }
        public decimal Conventional { get; set; }
    }
}
