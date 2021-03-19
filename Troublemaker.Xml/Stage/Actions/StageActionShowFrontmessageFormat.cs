using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowFrontmessageFormat']")]
    public sealed class StageActionShowFrontmessageFormat : StageAction
    {
        [XPath("@MessageColor")] public String MessageColor;

        [XPath("GameMessageForm")] public StageGameMessageForm GameMessageForm;
    }
}