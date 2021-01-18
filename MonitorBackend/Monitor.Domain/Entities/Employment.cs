using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Employment : BaseSiteEntity, IVisitEntity
    {
        public Employment(int siteId, DateTime visitDate)
            : base(siteId)
        {
            VisitDate = visitDate;
        }

        public DateTime VisitDate { get; private set; }

        public int Indirect { get; private set; }

        public int Direct { get; private set; }

        public void Set(int direct, int indirect)
        {
            Direct = direct;
            Indirect = indirect;
        }
    }
}
