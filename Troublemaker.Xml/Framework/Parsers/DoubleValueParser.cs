using System;
using System.Globalization;

namespace Troublemaker.Xml
{
    public sealed class DoubleValueParser : IValueParser
    {
        public static IValueParser Instance { get; } = new DoubleValueParser();

        public Object Parse(String value) => value == String.Empty ? Double.NaN : Double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}