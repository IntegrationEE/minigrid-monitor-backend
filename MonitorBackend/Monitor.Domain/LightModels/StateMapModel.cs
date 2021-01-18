using System.Collections.Generic;
using Monitor.Domain.Base;

namespace Monitor.Domain.LightModels
{
    public class StateMapModel : BaseLightModel
    {
        public StateMapModel()
        {
            Coordinates = new List<double[,]>();
        }

        public List<double[,]> Coordinates { get; set; }
    }
}
