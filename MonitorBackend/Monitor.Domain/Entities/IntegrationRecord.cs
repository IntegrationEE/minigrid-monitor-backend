using System;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class IntegrationRecord : BaseEntity
    {
        public IntegrationRecord(int integrationId, string value, IntegrationStatusCode status)
        {
            IntegrationId = integrationId;
            Value = value;
            Status = status;
        }

        public int IntegrationId { get; private set; }
        public virtual Integration Integration { get; private set; }

        public string Value { get; private set; }

        public IntegrationStatusCode Status { get; private set; }

        public int Inserted { get; private set; }

        public string Error { get; private set; }
        public int? StepId { get; private set; }
        public virtual IntegrationStep Step { get; private set; }

        public DateTime? EndDate { get; private set; }

        public void SetStatus(IntegrationStatusCode status, int inserted, string error, int? stepId, DateTime? endDate)
        {
            Status = status;
            Inserted = inserted;
            Error = error;
            EndDate = endDate;
            StepId = stepId;
        }
    }
}
