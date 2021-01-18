using System;
using Newtonsoft.Json;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Extensions;

namespace Monitor.Domain.LightModels
{
    public class IntegrationRecordLightModel : BaseViewModel
    {
        [JsonIgnore]
        public IntegrationStatusCode StatusCode { get; set; }
        public string Status => StatusCode.GetDescription();

        public int Inserted { get; set; }
        public string Error { get; set; }
        public string StepName { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
