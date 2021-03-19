using System;
using System.Collections;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlAttributesDeserializer : IDeserializer
    {
        private readonly Type _type;

        public XmlAttributesDeserializer(Type type)
        {
            _type = type;
        }

        public Boolean TryDeserialize(XPathNodeIterator navigator, out Object? result)
        {
            IDictionary dictionary = (IDictionary) Activator.CreateInstance(_type, navigator.Count);

            do
            {
                String key = navigator.Current.Name;
                String value = navigator.Current.Value;

                dictionary.Add(key, value);
            } while (navigator.MoveNext());

            result = dictionary;
            return true;
        }

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = (IDictionary) Activator.CreateInstance(_type, 0);
            return true;
        }
    }
}