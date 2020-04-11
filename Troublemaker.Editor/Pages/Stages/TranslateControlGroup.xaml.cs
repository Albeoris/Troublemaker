using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Troublemaker.Editor.ViewModels;

namespace Troublemaker.Editor.Pages
{
    /// <summary>
    /// Логика взаимодействия для TranslateControl.xaml
    /// </summary>
    public partial class TranslateControlGroup : UserControl
    {
        public TranslateControlGroup()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register(
            "Group", typeof(StageMessageGroup), typeof(TranslateControlGroup), new PropertyMetadata(default(StageMessageGroup)));

        public StageMessageGroup Group
        {
            get => (StageMessageGroup) GetValue(GroupProperty);
            set => SetValue(GroupProperty, value);
        }

        private void OnPreviewMouseWheel(Object sender, MouseWheelEventArgs e)
        {
            if (e.Handled)
                return;

            if (Group?.IsFocusable == true)
                return;

            e.Handled = true;
            MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            
            UIElement parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }
}