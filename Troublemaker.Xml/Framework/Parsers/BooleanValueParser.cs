using System;

namespace Troublemaker.Xml
{
    public sealed class BooleanValueParser : IValueParser
    {
        public static IValueParser Instance { get; } = new BooleanValueParser();

        public Object Parse(String value) => Boolean.Parse(value);

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}