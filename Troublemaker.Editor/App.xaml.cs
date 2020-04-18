using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using Troublemaker.Editor.Pages;
using Troublemaker.Editor.Settings;
using Troublemaker.Xml.Dialogs;
using Localization = ICSharpCode.AvalonEdit.Search.Localization;

namespace Troublemaker.Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Un();
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

        private void Un()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            // Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        // From all threads in the AppDomain.
        private void OnUnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception) e.ExceptionObject);
        }

        // /// <summary>
        // /// The DispatcherUnhandledException event is fired when an unhandled exception
        // /// is caught at the Dispatcher level (by the dispatcher).
        // /// </summary>
        // private void OnDispatcherUnhandledException(Object sender, DispatcherUnhandledExceptionEventArgs e)
        // {
        //     HandleException(e.Exception);
        // }

        /// <summary>
        /// Occurs when a faulted <see cref="System.Threading.Tasks.Task"/>'s unobserved exception is about to trigger exception escalation
        /// policy, which, by default, would terminate the process.
        /// </summary>
        /// <remarks>
        /// This AppDomain-wide event provides a mechanism to prevent exception
        /// escalation policy (which, by default, terminates the process) from triggering. 
        /// Each handler is passed a <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs"/>
        /// instance, which may be used to examine the exception and to mark it as observed.
        /// </remarks>
        private void TaskSchedulerOnUnobservedTaskException(Object? sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private void HandleException(Exception ex)
        {
            var message = "A critical error occurred during the operation of the program." + Environment.NewLine +
                          "Further work of the program may lead to data corruption. The application will be closed." + Environment.NewLine +
                          "Please press Ctrl+C to copy the exception details, and contact the author to fix the problem." + Environment.NewLine +
                          Environment.NewLine +
                          ex;

            MessageBox.Show(message, "Critical error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}