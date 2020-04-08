using System;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlArrayDeserializer : IDeserializer
    {
        private readonly Type _elementType;
        private readonly IDeserializer _elementDeserializer;

        public XmlArrayDeserializer(Type elementType, IDeserializer elementDeserializer)
        {
            _elementType = elementType;
            _elementDeserializer = elementDeserializer;
        }

        public Boolean TryDeserialize(XPathNodeIterator navigator, out Object? result)
        {
            Array array = Array.CreateInstance(_elementType, navigator.Count);
            Int32 index = 0;

            do
            {
                if (_elementDeserializer.TryDeserialize(navigator, out var value))
                    array.SetValue(value, index++);
                else
                    throw new NotSupportedException(navigator.Current.OuterXml);
            } while (navigator.MoveNext());

            result = array;
            return true;
        }

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = Array.CreateInstance(_elementType, 0);
            return true;
        }
    }
}