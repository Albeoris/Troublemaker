using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TitleMessage']")]
    public sealed class StageActionTitleMessage : StageAction, IMessageHandler
    {
        [XPath("@Image")] public String Image;
        [XPath("@Title")] public String TitleId;
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
            yield return ("Message", MessageId, null);
        }
    }
}