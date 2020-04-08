using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SystemMessage']")]
    public sealed class StageActionSystemMessage : StageAction, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        [XPath("@Title")] public String TitleId;
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
            yield return ("Message", MessageId, null);
        }
    }
}