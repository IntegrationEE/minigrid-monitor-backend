using System;
using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class NewService : BaseSiteEntity, IVisitEntity
    {
        public NewService(int siteId, DateTime visitDate)
            : base(siteId)
        {
            VisitDate = visitDate;
        }

        public DateTime VisitDate { get; private set; }

        public int Commercial { get; private set; }

        public int Productive { get; private set; }

        public int Health { get; private set; }

        public int Education { get; private set; }

        public void Set(int commercial, int productive, int health, int education)
        {
            Commercial = commercial;
            Productive = productive;
            Health = health;
            Education = education;
        }
    }
}
