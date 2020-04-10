using System;
using System.Collections.Generic;
using System.Linq;
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
using Troublemaker.Editor.Pages;

namespace Troublemaker.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            DataContext = StageController.Instance;
            InitializeComponent();

            Loaded += OnLoaded;
            Closed += OnClosed;
        }

        private void OnLoaded(Object sender, RoutedEventArgs e)
        {
            Activate();
        }

        private void OnClosed(Object? sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
        
        private void TreeViewItem_RequestBringIntoView(Object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void OnTabControlSelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1)
                throw new NotSupportedException("if (e.AddedItems.Count != 1)");

            TabItem selectedItem = (TabItem) e.AddedItems[0];
            StageController.Instance.LoadFiles((String)selectedItem.Header);
        }
    }
}
