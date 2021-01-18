using System;
using System.Linq;
using Monitor.Common.Models;
using Monitor.Common.Extensions;

namespace Monitor.Common.Logic
{
    public abstract class Converter : IConverter
    {
        protected string[] Prefixes;
        private const int EXPONENT = 3;

        public void Convert<TKey>(BaseChartViewModel<TKey> data)
        {
            data.SetPointsWithoutRounding();

            if (data.TemporaryPoints.Count == 0)
            { return; }

            var values = data.TemporaryPoints.Select(z => z.Value)
                .OrderByDescending(z => z)
                .Take(2)
                .ToArray();

            var maxValue = values[0];

            int degree = (int)Math.Floor(Math.Log10(Math.Abs((double)maxValue)) / EXPONENT);

            if (IsIncorrectSecondTheGreatestValue(values, maxValue, degree))
            { degree--; }

            if (IsIncorrectDegree(degree))
            { return; }

            degree = ValidatePrefixes(degree);

            data.Points = data.Points
                .Select(point => new PointViewModel<TKey, decimal>()
                {
                    Key = point.Key,
                    Value = GetValue(point.Value, degree).Round(data.Places)
                });

            data.SetUnitOfMeasure($"{Prefixes[degree - 1]}{data.UnitOfMeasure}");
        }

        public void Convert(ChartSeriesViewModel data)
        {
            data.SetPointsWithoutRounding();

            if (!data.Series.Any(z => z.Points.Count() > 0))
            { return; }

            var values = data.Series.SelectMany(z => z.Points.Select(y => y.Value))
                .OrderByDescending(z => z)
                .Take(2)
                .ToArray();

            var maxValue = values[0];

            var degree = (int)Math.Floor(Math.Log10(Math.Abs((double)maxValue)) / EXPONENT);

            if (IsIncorrectSecondTheGreatestValue(values, maxValue, degree))
            { degree--; }

            if (IsIncorrectDegree(degree))
            { return; }

            degree = ValidatePrefixes(degree);

            data.Series.ForEach(series =>
            {
                series.Points = series.Points
                    .Select(point => new PointViewModel<DateTime, decimal>()
                    {
                        Key = point.Key,
                        Value = GetValue(point.Value, degree).Round(series.Places)
                    });
            });

            data.SetUnitOfMeasure($"{Prefixes[degree - 1]}{data.UnitOfMeasure}");
        }

        private bool IsIncorrectDegree(int degree)
            => Math.Sign(degree) == -1 || degree == 0;

        private bool IsIncorrectSecondTheGreatestValue(decimal[] values, decimal maxValue, int degree)
            => Math.Pow(10, EXPONENT * degree) > (double)(values.Length == 1 ? maxValue : values[1]);

        private int ValidatePrefixes(int degree)
            => degree - 1 < Prefixes.Length ?
                degree :
                Prefixes.Length;

        private decimal GetValue(decimal value, int degree)
            => value * (decimal)Math.Pow(1000, -degree);
    }
}
