using Monitor.Domain.Base;

namespace Monitor.Domain.LightModels
{
    public class SiteDashboardModel : BaseViewModel
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string Programme { get; set; }

        public string Company { get; set; }

        public decimal RenewableCapacity { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }
    }
}
