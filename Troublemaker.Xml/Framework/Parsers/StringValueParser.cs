using System;

namespace Troublemaker.Xml
{
    public sealed class StringValueParser : IValueParser
    {
        public static IValueParser Instance { get; } = new StringValueParser();

        public Object Parse(String value) => value;

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}