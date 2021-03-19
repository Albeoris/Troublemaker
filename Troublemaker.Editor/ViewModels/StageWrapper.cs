using System;
using System.Collections.Generic;
using Troublemaker.Framework;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class StageWrapper : IStage
    {
        private readonly List<ExpandableCollection> _list;

        public StageWrapper(List<ExpandableCollection> list)
        {
            _list = list;
        }

        public Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            foreach (var item in _list)
                yield return (item.NodeName, item);
        }
    }
}