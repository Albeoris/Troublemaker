using System;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    public sealed class XmlDeserializationBinding
    {
        public XPathExpression Selector { get; }
        public IDeserializer Deserializer { get; }
        public IMemberSetter Setter { get; }

        public XmlDeserializationBinding(XPathExpression selector, IDeserializer deserializer, IMemberSetter setter)
        {
            Selector = selector;
            Deserializer = deserializer;
            Setter = setter;
        }

        public void Deserialize(XPathNavigator element, Object result)
        {
            XPathNodeIterator iterator = element.Select(Selector);
            if (!iterator.MoveNext())
            {
                if (Deserializer.TryGetDefaultValue(out Object? defaultValue))
                    Setter.SetValue(result, defaultValue);
                return;
            }

            if (Deserializer.TryDeserialize(iterator, out Object? value))
                Setter.SetValue(result, value);
        }
    }
}