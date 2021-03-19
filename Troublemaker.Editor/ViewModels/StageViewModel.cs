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
        public String Name { get; }
        public IReadOnlyList<StageExpandableViewModel> EnumerateExpandable { get; }

        public StageViewModel(String name, IStage stage)
        {
            Name = name;
            EnumerateExpandable = Filter(stage);
        }

        private static IReadOnlyList<StageExpandableViewModel> Filter(IStage stage)
        {
            StageExpandableCollectionViewModel[] result = Enumerate(stage).ToArray();
            if (result.Length == 1)
                return result[0].Rewrap(stage).ToArray();

            return result;
        }

        private static IEnumerable<StageExpandableCollectionViewModel> Enumerate(IStage stage)
        {
            foreach ((string name, IExpandable expandable) in stage.EnumerateChildren())
            {
                if (expandable is ExpandableCollection collection)
                {
                    var vm = new StageExpandableCollectionViewModel(stage, collection);
                    if (vm.Messages != null || vm.Components.Any())
                        yield return vm;
                }
                else
                {
                    throw new NotSupportedException(expandable.GetType().FullName);
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
