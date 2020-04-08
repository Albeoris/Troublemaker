using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public sealed class Localization
    {
        private readonly Dictionary<TextId, LocalizeString> _dic = new Dictionary<TextId, LocalizeString>(30000);

        public String Language { get; }

        public Localization(String language)
        {
            Language = language;
        }

        public void Add(TextId key, LocalizeString localizeString) => _dic.Add(key, localizeString);

        public LocalizeString this[TextId id] => TryGetValue(id, out var result) ? result : throw new KeyNotFoundException(id.ToString());
        public Boolean HasKey(TextId key) => _dic.ContainsKey(key);
        
        public Boolean TryGetValue(TextId key, out LocalizeString localizeString)
        {
            return _dic.TryGetValue(key, out localizeString);
        }

        public IEnumerable<LocalizeString> Enumerate() => _dic.Values;
    }
}