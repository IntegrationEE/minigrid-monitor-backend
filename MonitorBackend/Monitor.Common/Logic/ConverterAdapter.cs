using System.Linq;
using System.Collections.Generic;
using Monitor.Common.Enums;

namespace Monitor.Common.Logic
{
    public static class ConverterAdapter
    {
        private static readonly Dictionary<ConvertableType, IConverter> _converters = new Dictionary<ConvertableType, IConverter>
        {
            { ConvertableType.CURRENCY, new CurrencyConverter() },
            { ConvertableType.UNIT, new UnitConverter() }
        };

        public static IConverter GetConverter(ConvertableType? type)
        {
            return type.HasValue ?
                _converters.First(z => z.Key == type).Value :
                null;
        }
    }
}
