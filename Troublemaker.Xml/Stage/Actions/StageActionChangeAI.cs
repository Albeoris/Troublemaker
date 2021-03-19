using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeAI']")]
    public sealed class StageActionChangeAI : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
        [XPath("AIForm")] public StageAI AIForm;
    }
}