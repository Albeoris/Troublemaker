using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Troublemaker.Xml
{
    public class XmlDeserializerFactory
    {
        private static readonly Type ObjectType = typeof(Object);
        private static readonly Type ValueType = typeof(ValueType);

        public static XmlDeserializerTree Default { get; } = Build(typeof(Stage).Assembly);

        private static XmlDeserializerTree Build(Assembly assembly)
        {
            var source = new Dictionary<Type, List<(Int32, XmlComplexDeserializer)>>();
            var target = new Dictionary<Type, XmlNestedDeserializer>(source.Count);
            XmlDeserializerTree result = new XmlDeserializerTree(target);

            var toCompile = new List<(XPathAttribute, XmlComplexDeserializer)>();

            foreach (var type in assembly.GetTypes())
            {
                XPathAttribute? attribute = type.GetCustomAttribute<XPathAttribute>();
                if (attribute == null)
                    continue;

                List<(Int32, XmlComplexDeserializer)> list = Ensure(type, source);
                toCompile.Add((attribute, list.First().Item2));
            }

            foreach (var pair in source)
            {
                Type type = pair.Key;
                List<(Int32, XmlComplexDeserializer)> list = pair.Value;

                XmlComplexDeserializer[] array = list
                    .OrderByDescending(v => v.Item1)
                    .Select(v => v.Item2)
                    .ToArray();

                target.Add(type, new XmlNestedDeserializer(array));
            }

            foreach ((XPathAttribute attribute, XmlComplexDeserializer deserializer) in toCompile)
                deserializer.Compile(attribute, result);

            return result;
        }

        private static List<(Int32, XmlComplexDeserializer)> Ensure(Type type, Dictionary<Type, List<(Int32, XmlComplexDeserializer)>> dic)
        {
            if (dic.TryGetValue(type, out var list))
                return list;

            list = new List<(Int32, XmlComplexDeserializer)>();
            dic.Add(type, list);

            Int32 offset = 0;

            var deserializer = new XmlComplexDeserializer(type);
            list.Add((offset, deserializer));

            var baseType = type.BaseType;
            while (baseType != null && baseType != ObjectType && baseType != ValueType)
            {
                List<(Int32, XmlComplexDeserializer)> parent = Ensure(baseType, dic);
                parent.Add((++offset, deserializer));
                
                baseType = baseType.BaseType;
            }

            return list;
        }
    }
}