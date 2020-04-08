using System;

namespace Troublemaker.Xml
{
    public sealed class Int64ValueParser : IValueParser
    {
        public static IValueParser Instance { get; } = new Int64ValueParser();

        public Object Parse(String value) => Int64.Parse(value);

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}