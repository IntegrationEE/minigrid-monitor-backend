namespace Monitor.Common.Logic
{
    public class UnitConverter : Converter
    {
        public UnitConverter()
        {
            Prefixes = new string[] { "k", "M", "G", "T", "P", "E", "Z", "Y" };
        }
    }
}
