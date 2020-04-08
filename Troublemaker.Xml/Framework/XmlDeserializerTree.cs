using System;
using System.Collections.Generic;
using System.Xml.XPath;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public class XmlDeserializerTree
    {
        private readonly Dictionary<Type, XmlNestedDeserializer> _dic;

        public XmlDeserializerTree(Dictionary<Type, XmlNestedDeserializer> dic)
        {
            _dic = dic;
        }

        public T Deserialize<T>(String filePath)
        {
            XPathDocument doc = new XPathDocument(filePath);
            XPathNavigator navigator = doc.CreateNavigator();

            return Deserialize<T>(navigator);
        }

        public T Deserialize<T>(XPathNavigator navigator)
        {
            XPathNodeIterator iterator = navigator.Select("*");
            IDeserializer deserializer = Get(typeof(T));
            if (iterator.MoveNext() && deserializer.TryDeserialize(iterator, out var result))
                return (T) result;

            throw new NotSupportedException(typeof(T).FullName + Environment.NewLine + navigator.OuterXml);
        }

        public IDeserializer Get(Type type)
        {
            return _dic[type];
        }
        
        public XmlComplexDeserializer? FindBase(Type type)
        {
            if (_dic.TryGetValue(type, out var deserializer))
            {
                XmlNestedDeserializer nested = (XmlNestedDeserializer) deserializer;
                return nested.Last;
            }

            return null;
        }

        public IDeserializer GetDeserializer(Type type)
        {
            if (type.IsArray)
            {
                return GetArrayDeserializer(type);
            }

            if (type.IsGenericType)
            {
                Type generic = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();

                if (generic == typeof(IReadOnlyCollection<>))
                    return GetArrayDeserializer(genericArguments[0]);

                if (generic == typeof(IReadOnlyList<>))
                    return GetArrayDeserializer(genericArguments[0]);

                if (generic == typeof(IEnumerable<>))
                    return GetArrayDeserializer(genericArguments[0]);

                if (generic == typeof(Dictionary<,>))
                    return GetDictionaryDeserializer(type, genericArguments[0], genericArguments[1]);

                if (generic == typeof(IDictionary<,>))
                    return GetDictionaryDeserializer(type, genericArguments[0], genericArguments[1]);

                if (generic == typeof(Map<>))
                    return GetDictionaryDeserializer(type, typeof(String), genericArguments[0]);

                throw new NotSupportedException(type.FullName);
            }

            IValueParser valueParser;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Object:
                    return Get(type);
                case TypeCode.Boolean:
                    valueParser = BooleanValueParser.Instance;
                    break;
                case TypeCode.Int32:
                    valueParser = Int32ValueParser.Instance;
                    break;
                case TypeCode.UInt32:
                case TypeCode.Int64:
                    valueParser = Int64ValueParser.Instance;
                    break;
                case TypeCode.Double:
                    valueParser = DoubleValueParser.Instance;
                    break;
                case TypeCode.String:
                    valueParser = StringValueParser.Instance;
                    break;
                default:
                    throw new NotSupportedException(type.FullName);
            }

            return new XmlAttributeDeserializer(valueParser);
        }

        private IDeserializer GetArrayDeserializer(Type type)
        {
            var elementType = type.GetElementType() ?? throw new NotSupportedException(type.FullName);
            var elementDeserializer = GetDeserializer(elementType);
            return new XmlArrayDeserializer(elementType, elementDeserializer);
        }

        private IDeserializer GetDictionaryDeserializer(Type type, Type keyType, Type valueType)
        {
            var keyDeserializer = GetDeserializer(keyType);
            var valueDeserializer = GetDeserializer(valueType);
            return new XmlDictionaryDeserializer(type, keyDeserializer, valueDeserializer);
        }
    }
}