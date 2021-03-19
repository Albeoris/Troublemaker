using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='StatusVisible']")]
    public sealed class StageActionStatusVisible : StageAction
    {
        [XPath("@Visible")] public Boolean Visible;
    }
}