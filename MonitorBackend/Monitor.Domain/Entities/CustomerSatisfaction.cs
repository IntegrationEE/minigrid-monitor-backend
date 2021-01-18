using System;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class CustomerSatisfaction : BaseSiteEntity, IVisitEntity
    {
        public CustomerSatisfaction(int siteId, DateTime visitDate, int satisfaction, SatisfactionType type)
            : base(siteId)
        {
            VisitDate = visitDate;
            Satisfaction = satisfaction;
            Type = type;
        }

        public DateTime VisitDate { get; private set; }

        public int Satisfaction { get; private set; }

        public SatisfactionType Type { get; private set; }
    }
}
