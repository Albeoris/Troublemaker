using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SubInterfaceVisible']")]
    public sealed class StageActionSubInterfaceVisible : StageAction
    {
        [XPath("@Visible")] public Boolean Visible;
    }
}