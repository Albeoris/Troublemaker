using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Animation']")]
    public sealed class StageActionAnimation : StageAction
    {
        [XPath("@Animation")] public String Animation;
        [XPath("@Loop")] public Boolean Loop;

        [XPath("Unit")] public StagePointObject Unit;
    }
}