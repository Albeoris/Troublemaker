using System;
using System.Windows;
using Troublemaker.Editor.Pages;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Settings
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(TranslationFile translationFile)
        {
            UserCollection.Instance.Init(translationFile.Archive);
            DataContext = UserSettingsViewModel.Instance;
            InitializeComponent();

            Loaded += OnLoaded;
            Unloaded += OnClosed;
            Closed += OnClosed;
        }

        private void OnLoaded(Object sender, RoutedEventArgs e)
        {
            var us = UserSettingsViewModel.Instance;
            us.Bind(this);
            Loaded -= OnLoaded;
        }

        private void OnClosed(Object? sender, EventArgs e)
        {
            var us = UserSettingsViewModel.Instance;
            us.Unbind(this);
            Unloaded -= OnClosed;
            Closed -= OnClosed;
        }

        public static Boolean CheckAuthenticated()
        {
            if (UserSettingsViewModel.Instance.IsAuthenticated)
                return true;
            
            String language = StageController.Instance.SelectedHistory.Language;
            LoginWindow wnd = new LoginWindow(TranslationFile.Get(language));
            return wnd.ShowDialog() == true;
        }
    }
}