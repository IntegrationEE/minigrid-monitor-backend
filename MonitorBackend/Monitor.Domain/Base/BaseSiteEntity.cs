using Monitor.Domain.Entities;

namespace Monitor.Domain.Base
{
    public class BaseSiteEntity : BaseEntity, IBaseSiteEntity
    {
        public BaseSiteEntity() { }
        public BaseSiteEntity(int siteId)
            : base()
        {
            SiteId = siteId;
        }

        public int SiteId { get; private set; }
        public virtual Site Site { get; set; }
    }
}
