using System;
using System.Diagnostics.CodeAnalysis;

namespace Troublemaker.Xml
{
    public interface IValueParser
    {
        Object Parse(String value);
        Boolean TryGetDefaultValue([NotNullWhen(true)] out Object? defaultValue);
    }
}