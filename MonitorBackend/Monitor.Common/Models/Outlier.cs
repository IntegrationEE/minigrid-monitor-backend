using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monitor.Common.Models
{
    public class Outlier
    {
        private readonly List<string> _properties;

        public Outlier(int year, int month)
        {
            Year = year;
            Month = month;

            _properties = new List<string>();
            Properties = _properties.AsReadOnly();

            if (Month < 1 || Month > 12)
            {
                AddProperty(nameof(Month));
            }
        }

        public int Year { get; private set; }
        public int Month { get; private set; }
        public ReadOnlyCollection<string> Properties { get; private set; }

        public void AddProperty(string propertyName)
        {
            _properties.Add($"{char.ToLower(propertyName[0])}{propertyName.Substring(1)}");
        }
    }
}
