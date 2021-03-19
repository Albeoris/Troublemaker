using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class TranslationTags
    {
        private readonly List<String> _tags;
        private readonly List<String> _values;
        private readonly SubstringComparer _comparer = new SubstringComparer(StringComparison.Ordinal);

        private TranslationTags(List<String> tags, List<String> values)
        {
            _tags = tags;
            _values = values;
        }

        public IReadOnlyList<String> All => _tags;
        public IEnumerable<(String tag, String value)> Pairs => _tags.Zip(_values);

        public String? FindSingleTag(String substring) => Find(substring, _tags, StringComparer.Ordinal).SingleOrDefault();
        public IReadOnlyList<String> FindListTag(String substring) => Find(substring, _tags, _comparer);
        
        public String? FindSingleValue(String substring) => Find(substring, _values, StringComparer.Ordinal).SingleOrDefault();
        public IReadOnlyList<String> FindListValue(String substring) => Find(substring, _values, _comparer);

        private List<String> Find(String substring, List<String> list, IComparer<String> comparer)
        {
            List<String> result = new List<String>();
            
            Int32 index = _tags.BinarySearch(substring, comparer);
            if (index < 0)
                return result;

            for (; index < _tags.Count; index++)
            {
                String item = _tags[index];
                if (comparer.Compare(item, substring) == 0)
                    result.Add(list[index]);
                else
                    break;
            }

            return result;
        }

        public String Serialize()
        {
            StringBuilder sb = new StringBuilder(32 * 1024);

            String group = null;
            for (int i = 0; i < _tags.Count; i++)
            {
                var tag = _tags[i];
                var rep = _values[i];

                if (group is null)
                {
                    group = tag;
                }
                else if(!tag.StartsWith(group))
                {
                    sb.AppendLine();
                    sb.AppendLine();
                    group = tag;                    
                }

                sb.AppendLine($"{tag} = {rep}");
            }

            return sb.ToString();
        }
        
        public static TranslationTags Deserialize(String content)
        {
            List<(String, String)> data = new List<(String, String)>();
            if (String.IsNullOrEmpty(content))
                return new TranslationTags(new List<String>(), new List<String>());
            
            using (var sr = new StringReader(content))
            {
                while (true)
                {
                    String? line = sr.ReadLine();
                    if (line is null)
                        break;

                    String[] parts = line.Split('=');
                    if (parts.Length != 2)
                        continue;

                    var tag = parts[0].Trim();
                    var rep = parts[1].Trim();

                    if (String.IsNullOrEmpty(tag) || String.IsNullOrEmpty(rep))
                        continue;

                    data.Add((tag, rep));
                }
            }
            
            List<String> tags = new List<String>(data.Count);
            List<String> values = new List<String>(data.Count);
            foreach ((String tag, String value) in data.OrderBy(d => d.Item1, StringComparer.Ordinal))
            {
                tags.Add(tag);
                values.Add(value);
            }

            return new TranslationTags(tags, values);
        }
    }
}