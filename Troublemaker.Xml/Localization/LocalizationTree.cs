using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class LocalizationTree
    {
        private readonly Map<LocalizationTree> _map = new Map<LocalizationTree>();

        public String Name { get; }
        
        private TextReference _value;

        public LocalizationTree(String name)
        {
            Name = name;
        }

        public TextReference Value => _value == default ? throw new ArgumentNullException() : _value;
        public LocalizationTree this[String child] => _map[child];
        public Boolean TryGet(String child, out LocalizationTree result) => _map.TryGetValue(child, out result);
        public Boolean TryGet(Int32 childIndex, out LocalizationTree result) => _map.TryGetValue((childIndex + 1).ToString(), out result);

        public void Set(String path, TextReference value)
        {
            Int32 index = path.IndexOf('/');
            if (index > 0)
            {
                String name = path.Substring(0, index);
                String relative = path.Substring(index + 1);
                LocalizationTree child = _map.Ensure(name, () => new LocalizationTree(name));
                child.Set(relative, value);
            }
            else
            {
                LocalizationTree child = _map.Ensure(path, () => new LocalizationTree(path));
                child._value = value;
            }
        }
    }
}