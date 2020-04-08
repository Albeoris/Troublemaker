using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public interface IDeserializer
    {
        Boolean TryDeserialize(XPathNodeIterator navigator, [NotNullWhen(true)] out Object? result);
        Boolean TryGetDefaultValue([NotNullWhen(true)] out Object? defaultValue);
    }
}