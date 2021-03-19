using System;
using System.Windows;
using System.Windows.Controls;
using Troublemaker.Editor.ViewModels;

namespace Troublemaker.Editor.Pages
{
    public sealed class StageMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? StageMessageTemplate { get; set; }
        public DataTemplate? StageMessageGroupTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IStageMessage user = (IStageMessage) item;

            if (user is StageMessage)
                return StageMessageTemplate ?? throw new ArgumentNullException(nameof(StageMessageTemplate));

            if (user is StageMessageGroup)
                return StageMessageGroupTemplate ?? throw new ArgumentNullException(nameof(StageMessageGroupTemplate));

            throw new NotSupportedException(user.GetType().FullName);
        }
    }
}