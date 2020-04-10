using System;
using System.Collections.Generic;
using System.Xml;

namespace Troublemaker.Xml
{
    public sealed class LocalizationMap
    {
        private readonly Dictionary<String, TextReference> _map;
        private readonly Dictionary<TextId, List<String>> _back;

        public LocalizationTree Tree { get; } = new LocalizationTree(null);

        public LocalizationMap(String keymapPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(keymapPath);

            var nodes = doc.SelectNodes("/keymap/entry");
            _map = new Dictionary<String, TextReference>(nodes.Count);
            _back = new Dictionary<TextId, List<String>>(nodes.Count);

            foreach (XmlElement node in nodes)
            {
                var code = Int32.Parse(node.GetAttribute("code") ?? throw new NotSupportedException(node.OuterXml));
                var type = node.GetAttribute("dic_type") ?? throw new NotSupportedException(node.OuterXml);
                var oldKey = new TextId(type, code);
                var newKey = node.GetAttribute("key") ?? throw new NotSupportedException(node.OuterXml);

                TextReference reference = new TextReference(newKey, oldKey);
                _map.Add(newKey, reference);
                Tree.Set(newKey, reference);

                if (!_back.TryGetValue(oldKey, out var list))
                {
                    list = new List<String>(1);
                    _back.Add(oldKey, list);
                }

                list.Add(newKey);
            }
        }

        public TextId this[String reference] => _map[reference];

        public Boolean TryGetValue(String stringKey, out TextId numberKey)
        {
            if (_map.TryGetValue(stringKey, out var reference))
            {
                numberKey = reference;
                return true;
            }

            numberKey = default;
            return false;
        }
        
        public Boolean TryGetValue(TextId numberKey, out IReadOnlyList<String> stringKey)
        {
            var result = _back.TryGetValue(numberKey, out var list);
            stringKey = list;
            return result;
        }

        public static LocalizationMap Instance { get; private set; }

        public static void LoadMap(String keymapPath)
        {
            Instance = new LocalizationMap(keymapPath);
        }
    }
}