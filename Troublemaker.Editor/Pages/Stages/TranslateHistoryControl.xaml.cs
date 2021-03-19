using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Troublemaker.Editor.Annotations;
using Troublemaker.Editor.ViewModels;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public partial class TranslateHistoryControl : UserControl
    {
        public TranslateHistoryControl()
        {
            DataContext = StageController.Instance;
            InitializeComponent();

            Subscribe(StageController.Instance.SelectedHistory);
            StageController.Instance.SelectedHistoryChanged += OnSelectedHistoryChanged;
            TranslateLanguages.Instance.CurrentLanguagePropertyChanged += OnCurrentLanguagePropertyChanged;
            Unloaded += OnUnloaded;
        }

        private void OnCurrentLanguagePropertyChanged(Object? sender, EventArgs e)
        {
            TranslationHistory history = StageController.Instance.SelectedHistory;
            if (history == null)
                return;

            String language = TranslateLanguages.Instance.CurrentLanguage;
            TranslationFile file = TranslationFile.Get(language);
            StageController.Instance.SelectedHistory = file.GetHistory(history.Key);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            TranslateLanguages.Instance.CurrentLanguagePropertyChanged -= OnCurrentLanguagePropertyChanged;
            Unsubscribe(StageController.Instance.SelectedHistory);
        }

        private void OnSelectedHistoryChanged((TranslationHistory oldValue, TranslationHistory newValue) obj)
        {
            Unsubscribe(obj.oldValue);
            Subscribe(obj.newValue);
        }

        private void Subscribe(TranslationHistory? history)
        {
            if (history != null)
                history.Changed += OnHistoryUpdated;

            OnHistoryUpdated(history);
        }

        private void Unsubscribe(TranslationHistory? history)
        {
            if (history != null) history.Changed -= OnHistoryUpdated;
        }

        private void OnHistoryUpdated(TranslationHistory? history)
        {
            ItemsControl.ItemsSource = history?.EnumerateDescending;
        }
    }
}