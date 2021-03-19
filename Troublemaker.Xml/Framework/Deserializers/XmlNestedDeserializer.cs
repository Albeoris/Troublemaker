using System;
using System.Linq;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlNestedDeserializer : IDeserializer
    {
        private readonly XmlComplexDeserializer[] _array;
        
        public XmlNestedDeserializer(XmlComplexDeserializer[] array)
        {
            _array = array;
        }
        
        public XmlComplexDeserializer Last => _array.Last();

        public Boolean TryDeserialize(XPathNodeIterator navigator, out Object? result)
        {
            foreach (var deserializer in _array)
            {
                if (deserializer.TryDeserialize(navigator, out result))
                    return true;
            }

            result = default;
            return false;
        }

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }
    }
}