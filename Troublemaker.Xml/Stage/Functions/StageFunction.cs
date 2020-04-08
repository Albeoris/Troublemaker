using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Function")]
    public sealed class StageFunction : IExpandable
    {
        [XPath("@name")] public String Name;

        [XPath("Action")] public StageAction Action;
        [XPath("Parameter")] public StageFunctionParameter Parameter;

        public String NodeName => $"Function ({Name})";
        
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Action.Named(nameof(Action));
        }
    }
}