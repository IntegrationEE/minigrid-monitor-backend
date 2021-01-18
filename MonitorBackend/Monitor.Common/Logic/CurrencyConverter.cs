namespace Monitor.Common.Logic
{
    public class CurrencyConverter : Converter
    {
        public CurrencyConverter()
        {
            Prefixes = new string[] { "Thousands ", "Million ", "Billion ", "Trillion ", "Quadrillion " };
        }
    }
}
