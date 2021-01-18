using System.Collections.Generic;
using Monitor.Domain.Base;

namespace Monitor.Domain.LightModels
{
    public class FilterLightModel : BaseLightModel
    {
        public IEnumerable<int> SiteIds { get; set; }
    }
}
