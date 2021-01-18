using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.LightModels
{
    public class SiteCardModel : BaseLightModel
    {
        public string StateName { get; set; }
        public RenewableTechnology? RenewableTechnology { get; set; }
        public decimal RenewableCapacity { get; set; }
        public GridConnectionType GridConnection { get; set; }
        public ConventionalTechnology? ConventionalTechnology { get; set; }
        public SiteStatus Status { get; set; }
        public bool IsPublished { get; set; }
    }
}
