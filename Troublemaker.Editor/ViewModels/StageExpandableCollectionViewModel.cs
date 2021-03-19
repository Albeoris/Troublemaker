using System.Collections.Generic;
using System.Linq;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class StageExpandableCollectionViewModel : StageExpandableViewModel
    {
        private readonly IStage _stage;
        private readonly ExpandableCollection _expandables;

        public StageExpandableCollectionViewModel(IStage stage, ExpandableCollection expandables)
            :base(stage, expandables.NodeName, expandables)
        {
            _stage = stage;
            _expandables = expandables;
        }

        public override IEnumerable<StageExpandableViewModel> Components => Wrap(_stage, _expandables.EnumerateChildren());

        public IEnumerable<StageExpandableViewModel> Rewrap(IStage stage)
        {
            foreach (var child in _expandables.EnumerateChildren())
            {
                if (child.expandable is ExpandableCollection collection)
                {
                    var vm = new StageExpandableCollectionViewModel(stage, collection);
                    if (vm.Messages != null || vm.Components.Any())
                        yield return vm;
                }
                else
                {
                    var vm = new StageExpandableViewModel(stage, child.name, child.expandable);
                    if (vm.Messages != null)
                        yield return vm;
                }
            }
        }

        protected override StageMessageGroup? EnumerateMessagesInternal()
        {
            return null;
        }
    }
}