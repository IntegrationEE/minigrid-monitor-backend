using System;
using Monitor.Common.Enums;

namespace Monitor.HostedServices.Models
{
    internal class RecordStatusModel
    {
        public IntegrationStatusCode Status { get; set; }
        public int Inserted { get; set; }
        public string Error { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StepId { get; set; }
    }
}
