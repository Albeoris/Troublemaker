using System;

namespace Troublemaker.Xml
{
    public sealed class Int32ValueParser : IValueParser
    {
        public static IValueParser Instance { get; } = new Int32ValueParser();

        public Object Parse(String value) => Int32.Parse(value);

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}