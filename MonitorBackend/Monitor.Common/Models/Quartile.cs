namespace Monitor.Common.Models
{
    public class Quartile
    {
        public Quartile(double? q1, double? q3)
        {
            if (q1.HasValue && q3.HasValue)
            {
                double iqr = q3.Value - q1.Value;

                Min = (decimal)(q1 - iqr * 1.5);
                Max = (decimal)(q3 + iqr * 1.5);
            }
            else
            {
                Min = 0;
                Max = decimal.MaxValue;
            }
        }

        public decimal Min { get; private set; }
        public decimal Max { get; private set; }

        public bool IsOutlier(decimal? value)
        {
            return value.HasValue && (value.Value < Min || value.Value > Max);
        }
    }
}
