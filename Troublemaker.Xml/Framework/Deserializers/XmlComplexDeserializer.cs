using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.XPath;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class XmlComplexDeserializer : IDeserializer
    {
        public Type Type { get; }
        public XPathExpression Comparer { get; private set; }
        public XmlComplexDeserializer? Base { get; private set; }
        public List<XmlDeserializationBinding> Readers { get; } = new List<XmlDeserializationBinding>();

        public XmlComplexDeserializer(Type type)
        {
            Type = type;
        }

        public void Compile(XPathAttribute attribute, XmlDeserializerTree xmlDeserializers)
        {
            Comparer = attribute.Expression;
            Base = xmlDeserializers.FindBase(Type.BaseType);

            foreach (var field in Type.GetFields())
            {
                XPathAttribute? xpath = field.GetCustomAttribute<XPathAttribute>();
                if (xpath != null)
                {
                    XPathExpression selector = xpath.Expression;
                    IDeserializer reader = xmlDeserializers.GetDeserializer(field.FieldType, selector);
                    IMemberSetter setter = new ReflectionFieldSetter(field);
                    var binding = new XmlDeserializationBinding(selector, reader, setter);
                    Readers.Add(binding);
                }
            }

            foreach (var property in Type.GetProperties())
            {
                XPathAttribute? xpath = property.GetCustomAttribute<XPathAttribute>();
                if (xpath != null)
                {
                    XPathExpression selector = xpath.Expression;
                    IDeserializer reader = xmlDeserializers.GetDeserializer(property.PropertyType, selector);
                    IMemberSetter setter = new ReflectionPropertySetter(property);
                    var binding = new XmlDeserializationBinding(selector, reader, setter);
                    Readers.Add(binding);
                }
            }
        }
        
        public Boolean TryDeserialize(XPathNodeIterator element, [NotNullWhen(true)] out Object? result)
        {
            if (Type.IsAbstract || !element.Current.IsMatch(Comparer))
            {
                result = default;
                return false;
            }

            result = FormatterServices.GetUninitializedObject(Type);
            XmlComplexDeserializer deserializer = this;
            while (deserializer != null)
            {
                deserializer.DeserializeFields(element, result);
                deserializer = deserializer.Base;
            }
            return true;
        }

        private void DeserializeFields(XPathNodeIterator element, Object result)
        {
            foreach (XmlDeserializationBinding attr in Readers)
                attr.Deserialize(element.Current, result);
        }

        public Boolean TryGetDefaultValue(out Object? defaultValue)
        {
            defaultValue = default;
            return false;
        }

        public override String? ToString() => Type.FullName;
    }
}