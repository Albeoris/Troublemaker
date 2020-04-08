using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Troublemaker.Editor.Settings;
using Troublemaker.Xml;
using Troublemaker.Xml.Dialogs;

namespace Troublemaker.Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var path = Path.GetFullPath(@"Dictionary\keymap.dkm");
            if (!File.Exists(path))
            {
                MessageBox.Show($"File doesn't exist: {path}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
                return;
            }
            
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            new ResourceLoader(Environment.CurrentDirectory, null).Init();

            base.OnStartup(e);
        }
    }
}
