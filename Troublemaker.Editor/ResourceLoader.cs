using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Troublemaker.Editor.Framework;
using Troublemaker.Xml;

namespace Troublemaker.Editor
{
    public sealed class ResourceLoader
    {
        public String DirectoryPath { get; }
        public IProgressHandler Progress { get; }

        public ResourceLoader(String directoryPath, IProgressHandler progress)
        {
            DirectoryPath = directoryPath;
            Progress = progress;
        }

        public String LocalizationDirectory => Path.Combine(DirectoryPath, "Dictionary");
        public String XmlDirectory => Path.Combine(DirectoryPath, "Data", "xml");

        public void Init()
        {
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            ProgressWindow wnd = ProgressWindow.ShowBackground("Localization loading");
            try
            {
                String keymapPath = Path.Combine(LocalizationDirectory, "keymap.dkm");
                String[] directories = Directory.GetDirectories(LocalizationDirectory);
                wnd.SetTotal(directories.Length + 2);

                LocalizationMap.LoadMap(keymapPath);
                wnd.Increment(1);

                foreach (String child in directories)
                {
                    String keywordPath = Path.Combine(child, "dic_keyword.dic");
                    String textPath = Path.Combine(child, "dic_text.dic");

                    String language = Path.GetFileName(child);
                    Localize.Read(language, "Keyword", keywordPath);
                    Localize.Read(language, "Text", textPath);
                    wnd.Increment(1);
                }

                Localize.Remap();
                wnd.Increment(1);
            }
            finally
            {
                wnd.Close();
            }
        }
    }
}