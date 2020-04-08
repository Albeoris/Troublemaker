using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public class StageViewModel
    {
        private readonly Stage _stage;

        public StageViewModel(Stage stage)
        {
            _stage = stage;
        }

        public String Name => _stage.Map;

        public IEnumerable<StageExpandableCollectionViewModel> EnumerateExpandable
        {
            get
            {
                foreach ((string name, IExpandable expandable) in _stage.EnumerateChildren())
                {
                    if (expandable is ExpandableCollection collection)
                    {
                        var vm = new StageExpandableCollectionViewModel(_stage, collection);
                        if (vm.Messages != null || vm.Components.Any())
                            yield return vm;
                    }
                    else
                    {
                        throw new NotSupportedException(expandable.GetType().FullName);
                    }
                }
            }
        }

        // public IEnumerable<IStageComponentViewModel> Components => EnumerateComponents();
        //
        // private IEnumerable<IStageComponentViewModel> EnumerateComponents()
        // {
        //     yield return new StageDashboardsViewModel(_stage);
        //     yield return new StageObjectivesViewModel(_stage);
        //     yield return new StageMissionDirectsViewModel(_stage);
        // }
    }
}
