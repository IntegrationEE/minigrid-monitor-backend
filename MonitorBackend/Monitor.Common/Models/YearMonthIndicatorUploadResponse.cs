using System.Collections.Generic;

namespace Monitor.Common.Models
{
    public class YearMonthIndicatorUploadResponse
    {
        public YearMonthIndicatorUploadResponse()
        {
            Outliers = new List<Outlier>();
        }

        public int Inserted { get; set; }
        public int Updated { get; set; }
        public int NotApplicable { get; set; }

        public IList<Outlier> Outliers { get; set; }
    }
}
