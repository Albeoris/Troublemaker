using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class TranslationFile
    {
        private static readonly Cache<TranslationFile> ByLanguage = new Cache<TranslationFile>(lang => new TranslationFile(lang));

        public static TranslationFile Get(String language) => ByLanguage.Ensure(language);

        private readonly Localization _language;
        private readonly Dictionary<TextId, TranslationHistory> _histories = new Dictionary<TextId, TranslationHistory>();

        public TranslationFile(String language)
        {
            _language = Localize.GetDic(language);

            String archivePath = Path.GetFullPath($"Translation_{language}.zip");
            Archive = new StringArchive(archivePath);

            String? tagsContent = Archive.TryRead("Troublemaker/Tags.txt");
            Tags = TranslationTags.Deserialize(tagsContent);
        }

        public StringArchive Archive { get; }
        public TranslationTags Tags { get; }

        public TranslationHistory GetHistory(TextId key) => _histories.Ensure(key, EnsureTranslationHistory);
        public TranslationHistory? FindLoadedHistory(TextId key) => _histories.TryGetValue(key, out var result) ? result : default;

        public void EnsureLoaded(IReadOnlyList<TextId> keys)
        {
            TextId[] ids = keys.Distinct().Where(k => !_histories.ContainsKey(k)).ToArray();
            _histories.EnsureCapacity(ids.Length);
            Map<String> content = Archive.TryRead(ids.Select(id => id.FormatPath()).ToArray());
            foreach (var key in ids)
            {
                TranslationHistory history;
                if (content.TryGetValue(key.FormatPath(), out var serializedHistory))
                {
                    history = TranslationHistory.Deserialize(serializedHistory);
                }
                else
                {
                    history = CreateTranslationHistory(key);
                }

                _histories.Add(key, history);
            }
        }

        private TranslationHistory CreateTranslationHistory(TextId key)
        {
            LocalizeString defaultValue = Localize.Get(_language.Language, key);
            TranslationInfo info = new TranslationInfo(defaultValue.Text, default, default);
            return new TranslationHistory(_language.Language, key, info);
        }

        private TranslationHistory EnsureTranslationHistory(TextId key)
        {
            String? value = Archive.TryRead(key.FormatPath());
            if (value != null)
                return TranslationHistory.Deserialize(value);

            return CreateTranslationHistory(key);
        }
    }
}