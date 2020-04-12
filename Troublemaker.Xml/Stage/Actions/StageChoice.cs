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

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TextReference.Sentence(TitleId), null);
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}