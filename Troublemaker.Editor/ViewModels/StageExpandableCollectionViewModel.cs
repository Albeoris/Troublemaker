using System.Collections.Generic;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class StageExpandableCollectionViewModel : StageExpandableViewModel
    {
        private readonly Stage _stage;
        private readonly ExpandableCollection _expandables;

        public StageExpandableCollectionViewModel(Stage stage, ExpandableCollection expandables)
            :base(stage, expandables.NodeName, expandables)
        {
            _stage = stage;
            _expandables = expandables;
        }

        public override IEnumerable<StageExpandableViewModel> Components => Wrap(_stage, _expandables.EnumerateChildren());
        protected override StageMessageGroup? EnumerateMessagesInternal()
        {
            return null;
        }
    }
}