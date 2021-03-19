using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Troublemaker.Editor.Framework;
using Troublemaker.Xml;
using Path = System.IO.Path;

namespace Troublemaker.Editor.Pages
{
    public partial class TranslateLanguages : UserControl
    {
        public static TranslateLanguages Instance { get; private set; }

        public TranslateLanguages()
        {
            Instance = this;
            InitializeComponent();
            Loaded += TranslateLanguages_Loaded;
        }

        private void TranslateLanguages_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= TranslateLanguages_Loaded;
            CurrentLanguage = GetDefaultLanguage();
        }

        public static readonly DependencyProperty CurrentLanguageProperty = DependencyProperty.Register("CurrentLanguage", typeof(String), typeof(TranslateLanguages), new PropertyMetadata(default(String)));
        public static readonly DependencyPropertyDescriptor CurrentLanguagePropertyDescriptor = DependencyPropertyDescriptor.FromProperty(TranslateLanguages.CurrentLanguageProperty, typeof(TranslateLanguages));
        public event EventHandler CurrentLanguagePropertyChanged
        {
            add => CurrentLanguagePropertyDescriptor.AddValueChanged(this, value);
            remove => CurrentLanguagePropertyDescriptor.RemoveValueChanged(this, value);
        }

        public String CurrentLanguage
        {
            get => (String) GetValue(CurrentLanguageProperty);
            set => SetValue(CurrentLanguageProperty, value);
        }

        private String GetDefaultLanguage()
        {
            String language = Localize.HasDictionary("eng") ? "eng" : Localize.All.First().Language;

            DateTime lastChanged = default;
            foreach (String filePath in Directory.EnumerateFiles(Environment.CurrentDirectory, "Translation_???.zip"))
            {
                String? fileName = Path.GetFileName(filePath);
                if (fileName is null)
                    continue;

                String suffix = fileName.Substring("Translation_".Length, fileName.Length - "Translation_".Length - 4);
                if (!Localize.HasDictionary(suffix))
                    continue;
                
                var changed = File.GetLastWriteTimeUtc(filePath);
                if (changed > lastChanged)
                {
                    lastChanged = changed;
                    language = suffix;
                }
            }

            foreach (RadioButton button in EnumerateRadioButtons())
            {
                if (language == (String) button.Content)
                {
                    button.IsChecked = true;
                    break;
                }
            }

            return language;
        }

        public IEnumerable<RadioButton> EnumerateRadioButtons()
        {
            return LanguageButtons.FindVisualChildren<RadioButton>();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton) sender;
            String language = (String) button.Content;
            CurrentLanguage = language;
        }
    }
}