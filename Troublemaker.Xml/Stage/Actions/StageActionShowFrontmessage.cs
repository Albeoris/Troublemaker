using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowFrontmessage']")]
    public sealed class StageActionShowFrontMessage : StageAction, IMessageHandler
    {
        [XPath("@MessageColor")] public String MessageColor;
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Message", MessageId, null);
        }
    }
}