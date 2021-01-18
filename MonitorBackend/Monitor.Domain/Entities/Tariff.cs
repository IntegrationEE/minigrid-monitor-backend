using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Tariff : BaseSiteEntity, IVisitEntity
    {
        public Tariff(int siteId, DateTime visitDate)
            : base(siteId)
        {
            VisitDate = visitDate;
        }

        public DateTime VisitDate { get; private set; }

        public decimal Residential { get; private set; }

        public decimal Commercial { get; private set; }

        public decimal Public { get; private set; }

        public decimal Productive { get; private set; }

        public void Set(decimal residential, decimal commercial, decimal publicValue, decimal productive)
        {
            Residential = residential;
            Commercial = commercial;
            Public = publicValue;
            Productive = productive;
        }
    }
}
