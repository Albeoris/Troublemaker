using System;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlAttributeDeserializer : IDeserializer
    {
        private readonly IValueParser _parser;

        public XmlAttributeDeserializer(IValueParser parser)
        {
            _parser = parser;
        }

        public Boolean TryDeserialize(XPathNodeIterator navigator, out Object? result)
        {
            result = _parser.Parse(navigator.Current.Value);
            return true;
        }

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            return _parser.TryGetDefaultValue(out defaultValue);
        }
    }
}