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
using Troublemaker.Framework;
using Troublemaker.Xml;

namespace Troublemaker.View
{
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
            
            Object result =  Evaluate(name, value, rule);
            if (result is Object[])
                throw new NotSupportedException();
            return result;
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
                return _list.Lua.CallFunction(value, this, arg);
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

        public static LuaTable AllocateClassList<T>(this Lua lua, GetClassListDelegate link, Map<T> map, Map<XmlRule> schema, IdSpace idSpace) where T : IXmlObject
        {
            LuaTable table = lua.AllocateTable();
            
            foreach ((String name, IXmlObject obj) in map.Pairs)
            {
                LuaTable entry = lua.Allocate(link, obj.Attributes, schema);
                entry.SetIdspace(idSpace);

                table[name] = entry;
            }

            return table;
        }

        public static LuaTable Allocate(this Lua lua, GetClassListDelegate link, IReadOnlyDictionary<String, String> dictionary, Map<XmlRule> schema)
        {
            LuaTable table = lua.AllocateTable();
            HashSet<String> set = new HashSet<String>();

            foreach (var (key, value) in dictionary)
            {
                set.Add(key);

                if (schema.TryGetValue(key, out var rule))
                {
                    table[key] = ConvertValue(lua, link, value, rule);;
                    continue;
                }
                
                if (value == "false")
                {
                    table[key] = false;
                    continue;
                }
                
                if (value == "true")
                {
                    table[key] = true;
                    continue;
                }
                
                if (Double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                {
                    table[key] = number;
                    continue;
                }
                
                table[key] = value;
            }
            
            foreach (var (key, rule) in schema.Pairs)
            {
                if (set.Add(key))
                    table[key] = ConvertValue(lua, link, rule.Default, rule);
            }

            return table;
        }

        private static Object ConvertValue(Lua lua, GetClassListDelegate link, String value, XmlRule rule)
        {
            return rule.Type switch
            {
                "table" => value,
                "string" => value,
                "function" => value,
                "evaluated" => value,
                "link" => value, // ConvertLink(link, value, rule),
                "calculated" => ConvertCalculated(lua, value),
                "bool" => ConvertBoolean(value),
                "number" => ConvertNumber(value),
                _ => throw new NotSupportedException(rule.Type)
            };
        }

        private static Object ConvertLink(GetClassListDelegate link, String value, XmlRule rule)
        {
            if (value == "None")
                return null;
            
            return link(rule.Target)[value];
        }

        private static Object ConvertCalculated(Lua lua, String value)
        {
            if (value == null)
                return null;
            
            LuaFunction function = lua.GetFunction(value);
            if (function == null)
                throw new NotSupportedException(value);
            return function;
        }

        private static Object ConvertBoolean(String value)
        {
            return value switch
            {
                "false" => false,
                "true" => true,
                _ => value
            };
        }

        private static Object ConvertNumber(String value)
        {
            if (Double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                return number;

            return value;
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
            _lua.State.Encoding = Encoding.UTF8;
            
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
        public Object GetWithoutError(Object xmlObject, String key)
        {
            if (xmlObject is null)
                return null;

            if (xmlObject is String str && str == "None")
                return str;

            if (xmlObject is XmlClass obj)
                return obj.GetValue(key);

            throw new NotSupportedException(xmlObject.GetType().FullName);
        }
        
        [LuaFunction]
        public Boolean IsObject(Object xmlObject)
        {
            if (xmlObject is null)
                return false;

            if (xmlObject is String str && str == "None")
                return false;

            if (xmlObject is XmlClass)
                return true;

            throw new NotSupportedException(xmlObject.GetType().FullName);
        }
        
        [LuaFunction]
        public Object GetMasteryOwner(XmlClass mastery)
        {
            return null;
        }

        [LuaFunction]
        public String GetStringFontColorChangeTag(String color1, String text, String color2)
        {
            return $"[colour='{color1}']{text}[colour='{color2}']";
        }
        //
        // [LuaFunction]
        // public String FormatMessage(String formatMessage, LuaTable args)
        // {
        //      foreach (KeyValuePair<Object,Object> entry in args)
        //          formatMessage = formatMessage.Replace('$' + (String)entry.Key + '$', entry.Value?.ToString());
        //
        //     return formatMessage;
        // }

        [LuaFunction]
        public String KoreanPostpositionProcessCpp(String text)
        {
            return text;
        }
        
        [LuaFunction]
        public void LogAndPrint(params Object[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var arg in args)
                sb.AppendLine(arg?.ToString());
            throw new InvalidDataException(sb.ToString());
        }
        
        public void Exec()
        {
            _lua.DoFile(@"W:\Steam\steamapps\common\Troubleshooter\Data\CEGUI\datafiles\lua_scripts\client_Tooltip.lua");
            _lua.DoFile(@"W:\Steam\steamapps\common\Troubleshooter\Data\script\shared\shared_utility.lua");
            _lua.DoFile(@"W:\Steam\steamapps\common\Troubleshooter\Data\script\shared\shared_status.lua");
            _lua.DoFile(@"W:\Steam\steamapps\common\Troubleshooter\Data\script\shared\shared_tooltip.lua");
            _lua.DoFile(@"W:\Steam\steamapps\common\Troubleshooter\Data\script\shared\shared_ability.lua");

            foreach (String filePath in Directory.EnumerateFiles(@"W:\Steam\steamapps\common\Troubleshooter\Data\xml", "*.xml"))
            {
                if (!filePath.EndsWith("FilteringWord.xml") 
                    && !filePath.EndsWith("layout.xml")
                    && !filePath.EndsWith("PostEffect.xml")
                    && !filePath.EndsWith("testxml2.xml"))
                    _classes.Read(this, filePath);
            }

            XmlClassList masteries = _classes["Mastery"];
            XmlClass sharpBlade = masteries["Combatant"];

            // LuaFunction GetDeadText = _lua.GetFunction("GetDeadText");
            // var result = GetDeadText.Call(new Object[] {false}).Single();

            //var sharpBlade = _masteries["SharpBlade"];

            // LuaFunction GetStatusMessage = _lua.GetFunction("GetStatusMessage");
            // result = GetStatusMessage.Call(new Object[] {sharpBlade});
            
            // LuaFunction GetMasterySystemMessageText = _lua.GetFunction("GetMasterySystemMessageText");
            // result = GetMasterySystemMessageText.Call(new Object[] {sharpBlade});
            // result = GetMasterySystemMessageText.Call(new Object[] {sharpBlade});

            LuaTable keywords = (LuaTable)CallFunction("GetCustomKeywordTable", "Mastery");
            LuaFunction MasteryDesc = (LuaFunction)keywords["MasterySystemMessage"];
            var result = MasteryDesc.Call(new Object[] {sharpBlade});

            // var item = AllocateTable();
            // item["Mastery"] = sharpBlade;
            //
            // result = MasteryDesc.Call(new[] {item}).Single();

            Object[] masteryArgument = new Object[] {sharpBlade};

            String text = "$MasterySystemMessage$";
            Boolean changed = true;

            while (changed && text.Contains('$'))
            {
                changed = false;
                foreach (String key in keywords.Keys)
                {
                    String tag = $"${key}$";
                    if (text.Contains(tag))
                    {
                        LuaFunction function = (LuaFunction) keywords[key];
                        
                        String repl = (String)function.Call(masteryArgument).Single();
                        var newText = text.Replace(tag, repl);
                        changed = text != newText;
                        text = newText;
                    }
                }
            }

            Console.WriteLine(text);

        }

        public Object CallFunction(String name, params Object[] args)
        {
            return GetFunction(name).Call(args).Single();
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