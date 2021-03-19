using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public interface IExpandable
    {
        public String NodeName { get; }
        public Boolean CanFlatten => true;
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren();
    }
}