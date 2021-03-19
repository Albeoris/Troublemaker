using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using NLua;

namespace Troublemaker.View
{
    public static class ExtensionsIDictionary
    {
        public static TValue Ensure<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TValue> factory)
        {
            if (!dic.TryGetValue(key, out var value))
            {
                value = factory();
                dic[key] = value;
            }

            return value;
        }

        public static TValue Ensure<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> factory)
        {
            if (!dic.TryGetValue(key, out var value))
            {
                value = factory(key);
                dic[key] = value;
            }

            return value;
        }
    }
    
    public sealed class Cache<T> : Dictionary<String, T>
    {
        private readonly Func<String, T> _factory;

        public Cache(Func<String, T> factory)
            : base(StringComparer.Ordinal)
        {
            _factory = factory;
        }

        public T Ensure(String key)
        {
            return this.Ensure(key, _factory);
        }
    }

    public class Map<T> : Dictionary<String, T>, IEnumerable<T>
    {
        public Map()
        {
        }

        public Map(StringComparer? comparer = null)
            : base(comparer)
        {
        }

        public Map(Int32 capacity)
            : base(capacity)
        {
        }

        public Map(Int32 capacity, StringComparer comparer)
            : base(capacity, comparer)
        {
        }

        public IEnumerable<(String key, T value)> Pairs
        {
            get
            {
                Dictionary<String, T> dic = this;
                foreach (var pairs in dic)
                    yield return (pairs.Key, pairs.Value);
            }
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }

    public sealed class XmlRule
    {
        public String Type; // "table"
        public String SubType; // "link"
        public String Target; // "StatusType"
        public String Default; // "CalculatedProperty_MasteryTitle"
    }

    public sealed class XmlClassList
    {
        private readonly Map<XmlRule> _rules;
        private readonly Map<XmlClass> _classes;

        public LuaInstance Lua { get; }
        public String Id { get; }

        public XmlClassList(LuaInstance instance, String id, Map<XmlRule> rules, Map<XmlClass> classes)
        {
            Lua = instance;
            Id = id;
            _rules = rules;
            _classes = classes;
        }
        
        public XmlClass this[String name] => _classes[name];

        private LuaTable _table;

        public void Refresh()
        {
            if (_table != null)
            {
                _table.Dispose();
                _table = null;
            }
        }

        public LuaTable GetTable(Lua instance)
        {
            if (_table != null)
                return _table;

            LuaTable table = instance.AllocateTable();

            foreach ((String key, XmlClass value) in _classes.Pairs)
                table[key] = value;

            _table = table;
            return table;
        }

        public XmlRule FindRule(String name)
        {
            return _rules.TryGetValue(name, out var rule) ? rule : null;
        }
    }

    public sealed class XmlClasses
    {
        private readonly Map<XmlClassList> _lists = new Map<XmlClassList>();
        
        public XmlClasses()
        {
        }

        public XmlClassList this[String name] => _lists[name];

        public void Read(LuaInstance lua, String filePath)
        {
            foreach (XmlClassList list in XmlClassesReader.Read(lua, filePath))
                _lists.Add(list.Id, list);
        }

        public void Refresh()
        {
            foreach (XmlClassList list in _lists)
                list.Refresh();
        }
    }

    public sealed class XmlClassesReader
    {
        public static IEnumerable<XmlClassList> Read(LuaInstance lua, String filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlClassesReader reader = new XmlClassesReader(doc);
            return reader.Read(lua);
        }

        private readonly XmlDocument _doc;

        private XmlClassesReader(XmlDocument doc)
        {
            _doc = doc;
        }

        private IEnumerable<XmlClassList> Read(LuaInstance lua)
        {
            XmlNodeList idSpaces = _doc.SelectNodes("//idspace");
            foreach (XmlElement idSpace in idSpaces)
                yield return Read(lua, idSpace);
        }

        private XmlClassList Read(LuaInstance lua, XmlElement root)
        {
            String id = root.GetAttribute("id");
            
            Map<XmlRule> rules = ReadScheme(root);
            Map<XmlClass> classes = new Map<XmlClass>();
            XmlClassList list = new XmlClassList(lua, id, rules, classes);

            ReadClasses(root, list, classes);
            return list;
        }

        private void ReadClasses(XmlElement root, XmlClassList list, Map<XmlClass> result)
        {
            XmlNodeList classNodes = root.SelectNodes("class");
            foreach (XmlElement classNode in classNodes)
            {
                String name = classNode.GetAttribute("name");
                result[name] = ReadClass(classNode, list);
            }
        }

        private static Map<XmlRule> ReadScheme(XmlElement root)
        {
            Map<XmlRule> rules = new Map<XmlRule>();

            var schema = root["schema"];
            if (schema == null)
                return rules;

            foreach (XmlElement ruleNode in schema.ChildNodes.OfType<XmlElement>())
            {
                XmlRule rule = new XmlRule();

                foreach (XmlAttribute attribute in ruleNode.Attributes)
                {
                    var value = attribute.Value;

                    switch (attribute.Name)
                    {
                        case "property":
                            rules.Add(value, rule);
                            break;
                        case "type":
                            rule.Type = value;
                            break;
                        case "subtype":
                            rule.SubType = value;
                            break;
                        case "target":
                            rule.Target = value;
                            break;
                        case "default":
                            rule.Default = value;
                            break;
                    }
                }
            }

            return rules;
        }
        
        private static XmlClass ReadClass(XmlElement classNode, XmlClassList list)
        {
            Map<XmlValue> attributes = new Map<XmlValue>(classNode.Attributes.Count);
            foreach (XmlAttribute attribute in classNode.Attributes)
                attributes.Add(attribute.Name, new XmlValue(attribute.Value));

            Map<XmlValue> children = new Map<XmlValue>();
            foreach (XmlElement child in classNode.ChildNodes.OfType<XmlElement>())
            {
                String childName = child.Name;
                if (childName == "property")
                    continue; // TODO
                    
                children[childName] = ReadValue(child);
            }

            return new XmlClass(attributes, children, list);
        }

        private static XmlValue ReadValue(XmlElement classNode)
        {
            Map<XmlValue> attributes = new Map<XmlValue>(classNode.Attributes.Count);
            foreach (XmlAttribute attribute in classNode.Attributes)
                attributes.Add(attribute.Name, new XmlValue(attribute.Value));

            List<XmlValue> children = new List<XmlValue>();
            foreach (XmlElement child in classNode.ChildNodes.OfType<XmlElement>())
                children.Add(ReadValue(child));

            return new XmlValue(attributes, children);
        }
    }

    public sealed class XmlClass
    {
        private readonly Map<XmlValue> _attributes;
        private readonly Map<XmlValue> _children;
        private readonly XmlClassList _list;

        public XmlClass(Map<XmlValue> attributes, Map<XmlValue> children, XmlClassList list)
        {
            _attributes = attributes;
            _children = children;
            _list = list;
        }

        public String IdSpace => _list.Id;

        public Object this[String name] => GetValue(name);

        public Object GetValue(String name)
        {
            XmlRule rule = _list.FindRule(name);
            XmlValue value = FindValue(name, rule);
            
            return Evaluate(name, value, rule);
        }

        private Object Evaluate(String name, XmlValue value, XmlRule rule)
        {
            if (value == null)
                return null;

            if (rule == null)
                return ParseText();

            if (value.IsSimple)
            {
                return ConvertTo(name, value.Text, rule.Type, rule.SubType, rule.Target);
            }

            throw new NotSupportedException(rule.Type);
            
            Object ParseText()
            {
                String text = value.Text ?? throw new ArgumentNullException("value.Text");

                if (text == "false")
                    return false;
                if (text == "true")
                    return true;
                if (Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                    return number;

                return text;
            }
        }

        private Object ConvertTo(String arg, String value, String type, String subType, String target)
        {
            if (String.IsNullOrEmpty(value) || value == "None")
                return value;

            if (type == "table")
            {
                LuaTable table = _list.Lua.AllocateTable();
                table[0] = (Double) 0;
                String[] elements = value.Split(new[] {", "}, StringSplitOptions.None);

                for (Int32 i = 0; i < elements.Length; i++)
                {
                    String elem = elements[i];
                    table[i + 1] = (Double) ConvertTo(arg, elem, subType, null, target);
                }
                
                return table;
            }

            if (type == "bool")
            {
                if (value == "false")
                    return false;
                if (value == "true")
                    return true;
                throw new ArgumentException("value");
            }

            if (type == "number")
            {
                return Double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
            }

            if (type == "calculated")
            {
                return _list.Lua.GetFunction(value).Call(this, arg);
            }

            throw new NotSupportedException(type);
        }

        private XmlValue FindValue(String name, XmlRule rule)
        {
            XmlValue value;

            if (_attributes.TryGetValue(name, out value))
                return value;

            if (_children.TryGetValue(name, out value))
                return value;

            if (rule != null)
                return new XmlValue(rule.Default);

            return null;
        }
    }

    public sealed class XmlValue
    {
        private readonly String _value;
        private readonly Map<XmlValue> _attributes;
        private readonly List<XmlValue> _children;

        public Boolean IsSimple { get; }

        public XmlValue(String value)
        {
            _value = value;
            IsSimple = true;
        }

        public XmlValue(Map<XmlValue> attributes, List<XmlValue> children)
        {
            _attributes = attributes;
            _children = children;
            IsSimple = false;
        }

        public String Text => _value;
    }
    
    public struct IdSpace : IEquatable<IdSpace>
    {
        public static readonly IdSpace Color = new IdSpace(nameof(Color));
        public static readonly IdSpace Mastery = new IdSpace(nameof(Mastery));
        public static readonly IdSpace MasteryCategory = new IdSpace(nameof(MasteryCategory));
        public static readonly IdSpace MasteryType = new IdSpace(nameof(MasteryType));
        public static readonly IdSpace Status = new IdSpace(nameof(Status));

        public readonly String Value;

        private IdSpace(String value)
        {
            Value = value;
        }

        public static IdSpace Parse(String name)
        {
            return name switch
            {
                nameof(Color) => Color,
                nameof(Mastery) => Mastery,
                nameof(MasteryCategory) => MasteryCategory,
                nameof(MasteryType) => MasteryType,
                nameof(Status) => Status,
                _ => throw new NotSupportedException(name)
            };
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj is IdSpace space)
                return String.Equals(Value, space.Value);
            return false;
        }

        public Boolean Equals(IdSpace other) => Value == other.Value;
        public override String ToString() => Value;
        public override Int32 GetHashCode() => (Value != null ? Value.GetHashCode() : 0);
        public static Boolean operator ==(IdSpace left, IdSpace right) => left.Equals(right);
        public static Boolean operator !=(IdSpace left, IdSpace right) => !left.Equals(right);
    }

    public static class ExtensionsLuaTable
    {
        public static void SetIdspace(this LuaTable table, IdSpace space)
        {
            table["idspace"] = space.Value;
        }

        public static IdSpace GetIdspace(this LuaTable table)
        {
            return IdSpace.Parse((String)table["idspace"]);
        }
    }

    public static class ExtensionsLua
    {
        public static void RedirectFunctions<T>(this Lua lua, T instance)
        {
            Type type = typeof(T);

            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.GetCustomAttribute<LuaFunctionAttribute>() != null)
                    lua.RegisterFunction(method.Name, instance, method);
            }
        }

        public static LuaTable AllocateTable(this Lua lua)
        {
            String tableId = Guid.NewGuid().ToString();
            lua.NewTable(tableId);

            return lua.GetTable(tableId);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LuaFunctionAttribute : Attribute
    {
    }

    public sealed class LuaInstance : IDisposable
    {
        private readonly Lua _lua;
        private readonly Cache<LuaFunction> _functions;
        private readonly XmlClasses _classes = new XmlClasses();

        
        // private readonly LuaTable _colors;
        // private readonly LuaTable _masteries;
        // private readonly LuaTable _statuses;
        // private readonly LuaTable _masteriesCategory;
        // private readonly LuaTable _masteriesTypes;

        public LuaInstance()
        {
            _lua = new Lua();
            _lua.RedirectFunctions(this);
            
            _functions = new Cache<LuaFunction>(ResolveFunction);
            
            // _masteriesCategory = _lua.AllocateClassList(GetClassList, DB.Masteries.Categories, DB.Masteries.CategoriesSchema, IdSpace.MasteryCategory);
            // _masteriesTypes = _lua.AllocateClassList(GetClassList, DB.Masteries.Types, DB.Masteries.TypesSchema, IdSpace.MasteryType);
            // _colors = _lua.AllocateClassList(GetClassList, DB.Colors.Entries, DB.Colors.EntriesSchema, IdSpace.Color);
            // _masteries = _lua.AllocateClassList(GetClassList, DB.Masteries.Entries, DB.Masteries.EntriesSchema, IdSpace.Mastery);
            // _statuses = _lua.AllocateClassList(GetClassList, DB.Statuses.Entries, DB.Statuses.EntriesSchema, IdSpace.Status);
        }

        public void Dispose()
        {
            _lua.Dispose();
        }

        [LuaFunction]
        public String GetWord(String key)
        {
            return key;
        }

        [LuaFunction]
        public String GetIdspace(XmlClass xmlObject)
        {
            return xmlObject.IdSpace;
        }
        
        [LuaFunction]
        public LuaTable GetClassList(String idSpaceValue)
        {
            return _classes[idSpaceValue].GetTable(_lua);
        }

        [LuaFunction]
        public Object GetWithoutError(XmlClass xmlObject, String key)
        {
            return xmlObject.GetValue(key);
        }

        [LuaFunction]
        public String GetStringFontColorChangeTag(String color1, String text, String color2)
        {
            return $"[colour='{color1}']{text}[colour='{color2}']";
        }
        
        [LuaFunction]
        public String FormatMessage(String formatMessage, Object args)
        {
            // TODO
            
            // foreach (KeyValuePair<Object,Object> entry in args)
            //     formatMessage = formatMessage.Replace('$' + (String)entry.Key + '$', entry.Value?.ToString());

            return formatMessage;
        }
        
        [LuaFunction]
        public String KoreanPostpositionProcessCpp(String text)
        {
            return text;
        }

        private StringBuilder _str = new StringBuilder();
        
        [LuaFunction]
        public void LogAndPrint(params Object[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var arg in args)
                sb.AppendLine(arg?.ToString());
            Console.WriteLine(sb.ToString());
            _str.AppendLine(sb.ToString());
            _str.AppendLine();
            // throw new InvalidDataException(sb.ToString());
        }
        
        public void Exec()
        {
            _lua.DoFile(@"Data\script\shared\shared_status.lua");
            _lua.DoFile(@"Data\script\shared\shared_tooltip.lua");
            _lua.DoFile(@"Data\script\shared\shared_ability.lua");

            foreach (String filePath in Directory.EnumerateFiles(@"Data\xml", "*.xml"))
            {
                if (!filePath.EndsWith("FilteringWord.xml") 
                    && !filePath.EndsWith("layout.xml")
                    && !filePath.EndsWith("PostEffect.xml")
                    && !filePath.EndsWith("testxml2.xml"))
                    _classes.Read(this, filePath);
            }

            XmlClassList masteries = _classes["Mastery"];
            XmlClass sharpBlade = masteries["SharpBlade"];

            LuaFunction GetDeadText = _lua.GetFunction("GetDeadText");
            var result = GetDeadText.Call(new Object[] {false});

            //var sharpBlade = _masteries["SharpBlade"];

            LuaFunction GetStatusMessage = _lua.GetFunction("GetStatusMessage");
            result = GetStatusMessage.Call(new Object[] {sharpBlade});
            
            LuaFunction GetMasterySystemMessageText = _lua.GetFunction("GetMasterySystemMessageText");
            result = GetMasterySystemMessageText.Call(new Object[] {sharpBlade});
        }
        
        public LuaFunction GetFunction(String name)
        {
            return _functions.Ensure(name);
        }
        
        private LuaFunction ResolveFunction(String name)
        {
            LuaFunction function = _lua.GetFunction(name);
            if (function == null)
                throw new ArgumentException(name);
            return function;
        }
        
        public LuaTable AllocateTable()
        {
            return _lua.AllocateTable();
        }
    }
    
    public delegate LuaTable GetClassListDelegate(String idSpaceValue);
}