using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[not(@Type)]")]
    public sealed class StageActionInvalid : StageAction
    {
    }
}