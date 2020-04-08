using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Troublemaker.Xml
{
    public static class Localize
    {
        private static readonly Dictionary<String, Localization> _perLanguage;

        static Localize()
        {
            _perLanguage = new Dictionary<String, Localization>();
        }

        public static IReadOnlyCollection<Localization> All => _perLanguage.Values;

        public static Boolean HasDictionary(String language)
        {
            return _perLanguage.ContainsKey(language);
        }
        
        public static Localization GetDic(String language)
        {
            return _perLanguage[language];
        }
        
        public static Boolean HasKey(TextId id) => All.Any(z => z.HasKey(id));
        public static LocalizeString Get(String language, TextId key) => _perLanguage[language][key];
        
        private static readonly Regex TagsRegex = new Regex(@"(\[![^]]+\]+)*(.+)", RegexOptions.Compiled);

        public static void Read(String language, String type, String path)
        {
            if (!_perLanguage.TryGetValue(language, out var dic))
            {
                dic = new Localization(language);
                _perLanguage.Add(language, dic);
            }

            String[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                String[] parts = line.Split('\t');
                if (parts.Length != 3)
                    throw new NotSupportedException(line);

                var code = Int32.Parse(parts[0].Substring(1));
                TextId key = new TextId(type, code);
                var comment = parts[1];
                var text = parts[2];
                var match = TagsRegex.Match(text);
                if (match.Success && match.Groups[1].Success)
                    text = match.Groups[2].Value;
                dic.Add(key, new LocalizeString(key, text, comment));
            }
        }

        public static void Remap()
        {
            if (_perLanguage.TryGetValue("rus", out var rus) &&
                _perLanguage.TryGetValue("kor", out var kor) &&
                _perLanguage.TryGetValue("eng", out var eng))
            {
                foreach (var rusLine in rus.Enumerate())
                {
                    LocalizeString korLine = kor[rusLine.Key];
                    if (rusLine.Text == korLine.Text)
                        rusLine.Text = eng[rusLine.Key].Text;
                }
            }
        }
    }
}