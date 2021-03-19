using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class TranslationHistory
    {
        private readonly LinkedList<TranslationInfo> _infos = new LinkedList<TranslationInfo>();

        public event Action<TranslationHistory> Changed;

        public TranslationHistory(String language, TextId key, TranslationInfo defaultText)
        {
            Language = language;
            Key = key;
            AddLast(defaultText);

            Changed += OnChanged;
        }

        public String Language { get; }
        public TextId Key { get; }
        public String CurrentText { get; set; }
        public TranslationInfo Last => _infos.RequireLastValue();
        public Boolean HasChanges => CurrentText != Last.Text;

        public IEnumerable<TranslationInfo> EnumerateDescending
        {
            get
            {
                var node = _infos.Last;
                while (node != null)
                {
                    yield return node.Value;
                    node = node.Previous;
                }
            }
        }

        public void SaveChanges()
        {
            if (!HasChanges)
                throw new InvalidOperationException("Has no changes.");

            TranslationInfo last = _infos.RequireLastValue();
            AddLast(last.CreateEdit(CurrentText));

            Changed?.Invoke(this);
        }

        private void AddLast(TranslationInfo defaultText)
        {
            _infos.AddLast(defaultText);
            CurrentText = defaultText.Text;
        }

        public String Serialize()
        {
            StringBuilder sb = new StringBuilder(4096);
            using (var sw = new StringWriter(sb))
            {
                sw.WriteLine(Language);
                sw.WriteLine(Key.FormatPath());

                foreach (TranslationInfo info in _infos)
                    info.Serialize(sw);
            }
            return sb.ToString();
        }

        public static TranslationHistory Deserialize(String serializedHistory)
        {
            using (StringReader sr = new StringReader(serializedHistory))
            {
                String language = sr.RequireLine();
                TextId key = TextId.ParsePath(sr.RequireLine());

                if (!TranslationInfo.TryDeserialize(sr, out TranslationInfo info))
                    throw new NotSupportedException(serializedHistory);

                TranslationHistory history = new TranslationHistory(language, key, info);
                while (TranslationInfo.TryDeserialize(sr, out info))
                    history.AddLast(info);

                return history;
            }
        }

        public void Select(TranslationInfo info)
        {
            if (!CanSelect(info)) throw new InvalidOperationException("Select command cannot be executed.");

            var node = _infos.Get(info);
            _infos.Remove(node);
            AddLast(info);

            CurrentText = Last.Text;
            Changed?.Invoke(this);
        }

        public void Approve(TranslationInfo info)
        {
            if (!CanApprove(info)) throw new InvalidOperationException("Approve command cannot be executed.");

            info.Approve();

            CurrentText = Last.Text;
            Changed?.Invoke(this);
        }

        public void Disapprove(TranslationInfo info)
        {
            if (!CanDisapprove(info)) throw new InvalidOperationException("Disapprove command cannot be executed.");

            info.Disapprove();

            Changed?.Invoke(this);
        }

        public void Delete(TranslationInfo info)
        {
            if (!CanDelete(info)) throw new InvalidOperationException("Delete command cannot be executed.");

            var node = _infos.Get(info);
            _infos.Remove(node);

            Changed?.Invoke(this);
        }

        public Boolean CanSelect(TranslationInfo info)
        {
            return Last != info && Last.Approved == default;
        }

        public Boolean CanApprove(TranslationInfo info)
        {
            return Last == info && info.Approved == default;
        }

        public Boolean CanDisapprove(TranslationInfo info)
        {
            return Last == info && info.Approved != default;
        }

        public Boolean CanDelete(TranslationInfo info)
        {
            return Last != info;
        }

        private static void OnChanged(TranslationHistory obj)
        {
            String archivePath = Path.GetFullPath($"Translation_{obj.Language}.zip");
            StringArchive archive = new StringArchive(archivePath);
            archive.Write(obj.Key.FormatPath(), obj.Serialize());
        }
    }
}