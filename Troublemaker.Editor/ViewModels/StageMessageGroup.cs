using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class StageMessageGroup : IStageMessage
    {
        public String Name { get; }
        public IStageMessage[] Messages { get; }
        
        public Brush Foreground { get; set; } = Brushes.Black;
        public Brush Background { get; set; } = Brushes.Transparent;
        public ScrollBarVisibility IsScrollable { get; set; } = ScrollBarVisibility.Disabled;
        public Boolean IsFocusable => IsScrollable == ScrollBarVisibility.Visible || IsScrollable == ScrollBarVisibility.Auto;

        public StageMessageGroup(String name, IStageMessage[] messages)
        {
            Name = name;
            Messages = messages;
        }
    }
}