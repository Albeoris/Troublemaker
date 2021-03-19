using System;
using System.Collections;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlDictionaryDeserializer : IDeserializer
    {
        private readonly Type _type;
        private readonly IDeserializer _keyDeserializer;
        private readonly IDeserializer _valueDeserializer;

        public XmlDictionaryDeserializer(Type type, IDeserializer keyDeserializer, IDeserializer valueDeserializer)
        {
            _type = type;
            _keyDeserializer = keyDeserializer;
            _valueDeserializer = valueDeserializer;
        }

        public Boolean TryDeserialize(XPathNodeIterator navigator, out Object? result)
        {
            IDictionary dictionary = (IDictionary) Activator.CreateInstance(_type, navigator.Count);

            do
            {
                if (!_keyDeserializer.TryDeserialize(navigator, out var key))
                    throw new NotSupportedException(navigator.Current.OuterXml);

                var parent = navigator.Current.Clone();
                if (!parent.MoveToParent())
                    throw new NotSupportedException(navigator.Current.OuterXml);

                XPathNodeIterator parentNavigator = parent.Select(".");
                if (!parentNavigator.MoveNext() || !_valueDeserializer.TryDeserialize(parentNavigator, out var value))
                    throw new NotSupportedException(parent.OuterXml);

                if (key is String stringKey && stringKey == String.Empty)
                    key = $"<Generated_{dictionary.Count}>";

                dictionary[key] = value;
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