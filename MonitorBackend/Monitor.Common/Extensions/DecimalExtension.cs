using System;

namespace Monitor.Common.Extensions
{
    public static class DecimalExtension
    {
        public static decimal Round(this decimal decimalValue, int decimalPlaces = 2)
            => Math.Round(decimalValue, decimalPlaces);

        public static decimal ToPercentage(this decimal decimalValue, decimal total, int decimalPlaces = 2)
            => total > 0 ? (decimalValue * Constants.HUNDRED / total).Round(decimalPlaces) : 0;

        public static decimal? MultiplyByThousand(this decimal? decimalValue)
            => decimalValue.HasValue ? (decimal?)(decimalValue.Value * Constants.THOUSAND) : null;

        public static decimal? DividingByThousand(this decimal? decimalValue)
            => decimalValue.HasValue ? (decimal?)(decimalValue.Value / Constants.THOUSAND) : null;
    }
}
