namespace Monitor.Common.Models
{
    public class YearMonthIndicatorValidateResponse
    {
        public YearMonthIndicatorValidateResponse(Outlier outliers)
        {
            Outliers = outliers;
        }

        public bool IsValid => Outliers == null;
        public Outlier Outliers { get; set; }
    }
}
