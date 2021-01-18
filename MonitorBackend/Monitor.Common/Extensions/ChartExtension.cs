using System.Linq;
using System.Collections.Generic;

namespace Monitor.Common.Extensions
{
    public static class ChartExtension
    {
        public static void AddPoint(this List<(string key, decimal value)> points, string label, decimal value, decimal total)
        {
            const int DECIMAL_PLACES = 0;

            value = value.ToPercentage(total, DECIMAL_PLACES);

            if (value > 0)
            {
                points.Add((label, value));
            }
        }

        public static bool IsValidSeries(this IEnumerable<(int Year, decimal Value)> data)
            => data.Any(z => z.Value > 0);

        public static bool IsValidSeries(this IEnumerable<(int Year, int Month, decimal Value)> data)
            => data.Any(z => z.Value > 0);
    }
}
