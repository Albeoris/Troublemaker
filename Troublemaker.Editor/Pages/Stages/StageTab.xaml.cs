using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    /// <summary>
    /// Логика взаимодействия для MasteryTab.xaml
    /// </summary>
    public partial class StageTab
    {
        public static StageTab Instance { get; private set; }

        public StageTab()
        {
            Instance = this;
            DataContext = StageController.Instance;
            InitializeComponent();
        }

        private void TreeViewItem_RequestBringIntoView(Object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}
