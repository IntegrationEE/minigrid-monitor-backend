using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class PeopleConnected : BaseSiteEntity, IVisitEntity
    {
        public PeopleConnected(int siteId, DateTime visitDate)
            : base(siteId)
        {
            VisitDate = visitDate;
        }

        public DateTime VisitDate { get; private set; }

        public int Productive { get; private set; }

        public int Commercial { get; private set; }

        public int Residential { get; private set; }

        public int Public { get; private set; }

        public void Set(int productive, int commercial, int residential, int publicValue)
        {
            Productive = productive;
            Commercial = commercial;
            Residential = residential;
            Public = publicValue;
        }
    }
}
