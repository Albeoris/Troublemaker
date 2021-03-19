using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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
using Troublemaker.Editor.Framework;
using Troublemaker.Editor.Settings;
using Troublemaker.Editor.ViewModels;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public partial class TranslateHistoryItem : UserControl
    {
        public TranslateHistoryItem()
        {
            SelectClick = new SelectCommand(this);
            ApproveClick = new ApproveCommand(this);
            DisapproveClick = new DisapproveCommand(this);
            DeleteClick = new DeleteCommand(this);
            InitializeComponent();
        }

        private void RefreshCommands()
        {
            TitleBlock.Visibility = String.IsNullOrEmpty(Item?.Title) ? Visibility.Collapsed : Visibility.Visible;

            SelectButton.Visibility = SelectClick.CanExecute(null) ? Visibility.Visible : Visibility.Collapsed;
            SelectClick.RaiseCanExecuteChanged();
            
            ApproveButton.Visibility = ApproveClick.CanExecute(null) ? Visibility.Visible : Visibility.Collapsed;
            ApproveClick.RaiseCanExecuteChanged();
            
            DisapproveButton.Visibility = DisapproveClick.CanExecute(null) ? Visibility.Visible : Visibility.Collapsed;
            DisapproveClick.RaiseCanExecuteChanged();
            
            DeleteButton.Visibility = DeleteClick.CanExecute(null) ? Visibility.Visible : Visibility.Collapsed;
            DeleteClick.RaiseCanExecuteChanged();
        }

        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(TranslationInfo), typeof(TranslateHistoryItem), new PropertyMetadata(default(TranslationInfo), OnItemChanged));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (TranslateHistoryItem) d;
            item.RefreshCommands();
        }

        public TranslationInfo Item
        {
            get => (TranslationInfo) GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public SelectCommand SelectClick { get; }
        public ApproveCommand ApproveClick { get; }
        public DisapproveCommand DisapproveClick { get; }
        public DeleteCommand DeleteClick { get; }

        public sealed class SelectCommand : BaseCommand
        {
            private readonly TranslateHistoryItem _control;

            public SelectCommand(TranslateHistoryItem control)
            {
                _control = control;
            }

            public override Boolean CanExecute(Object parameter)
            {
                return StageController.Instance.SelectedHistory.CanSelect(_control.Item);
            }

            public override void Execute(Object parameter)
            {
                if (!LoginWindow.CheckAuthenticated()) return;
                
                StageController.Instance.SelectedHistory.Select(_control.Item);
            }
        }

        public sealed class ApproveCommand : BaseCommand
        {
            private readonly TranslateHistoryItem _control;

            public ApproveCommand(TranslateHistoryItem control)
            {
                _control = control;
            }

            public override Boolean CanExecute(Object parameter)
            {
                return StageController.Instance.SelectedHistory.CanApprove(_control.Item);
            }

            public override void Execute(Object parameter)
            {
                if (!LoginWindow.CheckAuthenticated()) return;
                
                StageController.Instance.SelectedHistory.Approve(_control.Item);
            }
        }

        public sealed class DisapproveCommand : BaseCommand
        {
            private readonly TranslateHistoryItem _control;

            public DisapproveCommand(TranslateHistoryItem control)
            {
                _control = control;
            }

            public override Boolean CanExecute(Object parameter)
            {
                return StageController.Instance.SelectedHistory.CanDisapprove(_control.Item);
            }

            public override void Execute(Object parameter)
            {
                if (!LoginWindow.CheckAuthenticated()) return;
                
                StageController.Instance.SelectedHistory.Disapprove(_control.Item);
            }
        }

        public sealed class DeleteCommand : BaseCommand
        {
            private readonly TranslateHistoryItem _control;

            public DeleteCommand(TranslateHistoryItem control)
            {
                _control = control;
            }

            public override Boolean CanExecute(Object parameter)
            {
                return StageController.Instance.SelectedHistory.CanDelete(_control.Item);
            }

            public override void Execute(Object parameter)
            {
                if (!LoginWindow.CheckAuthenticated()) return;
                
                StageController.Instance.SelectedHistory.Delete(_control.Item);
            }
        }
    }
}
