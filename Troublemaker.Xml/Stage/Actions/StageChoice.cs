using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Choice")]
    public sealed class StageChoice : IExpandable, IMessageHandler
    {
        [XPath("@DBKey")] public String DBKey;
        [XPath("@Title")] public String TitleId;
        [XPath("@Message")] public String MessageId;
        [XPath("@Notice")] public String Notice;

        [XPath("ActionList/*")] public StageAction[] ActionList;
        [XPath("SwitchEnvironment/*")] public StageEnvironment[] SwitchEnvironment;

        public String NodeName => $"Choice ({DBKey})";

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return ActionList.Named(nameof(ActionList));
        }

        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
            yield return ("Message", MessageId, null);
        }
    }
}