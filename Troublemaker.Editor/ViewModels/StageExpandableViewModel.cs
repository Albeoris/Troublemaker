using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public class StageExpandableViewModel
    {
        private readonly IStage _stage;
        private readonly IExpandable _expandable;

        public String Name { get; }

        protected StageExpandableViewModel(IStage stage, String name, IExpandable expandable)
        {
            _stage = stage;
            _expandable = expandable;

            Name = name;
            Messages = EnumerateMessagesInternal();
        }
        
        protected static IEnumerable<StageExpandableViewModel> Wrap(IStage stage, IEnumerable<(String name, IExpandable expandable)> children)
        {
            foreach (var child in children)
            {
                var vm = new StageExpandableViewModel(stage, child.name, child.expandable);
                if (vm.Messages != null)
                    yield return vm;
            }
        }

        public virtual IEnumerable<StageExpandableViewModel> Components { get; }
        public StageMessageGroup? Messages { get; }

        protected virtual StageMessageGroup? EnumerateMessagesInternal()
        {
            MessageBuilder root = new MessageBuilder(0, Name);
            
            Stack<MessageBuilder> builders = new Stack<MessageBuilder>();
            builders.Push(root);
            
            foreach ((Int32 level, IExpandable? expandable) in _expandable.EnumerateChildrenRecursively(true).ToArray())
            {
                if (expandable == ExpandableEnd.Instance)
                {
                    if (!builders.TryPop(out _))
                        throw new InvalidOperationException("!builders.TryPop");
                    continue;
                }

                if (expandable.NodeName == "VictoryCondition")
                {
                    
                }

                var builder = builders.Peek();
                if (builder.Level > level || level - builder.Level > 1)
                    throw new InvalidOperationException("builder.Level > level || level - builder.Level > 1");

                if (builder.Level < level)
                {
                    builder = builder.Child(expandable.NodeName);
                    builders.Push(builder);
                }

                if (expandable is IMessageHandler messageHandler)
                    builder.AddMessage(messageHandler, _stage);
            }

            if (!builders.TryPop(out var last) || builders.Count != 0 || last != root)
                throw new InvalidOperationException("!builders.TryPop(out var last) || builders.Count != 0 || last != root");

            if (!root.TryBuild(out var result))
                return null;

            if (result is StageMessageGroup group)
            {
                group.IsScrollable = ScrollBarVisibility.Visible;
                return @group;
            }

            return new StageMessageGroup(Name, new[] {result}) {IsScrollable = ScrollBarVisibility.Visible};
        }
    }
}