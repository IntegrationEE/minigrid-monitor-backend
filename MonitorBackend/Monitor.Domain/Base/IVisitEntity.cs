using System;

namespace Monitor.Domain.Base
{
    public interface IVisitEntity : IBaseSiteEntity
    {
        public DateTime VisitDate { get; }
    }
}
