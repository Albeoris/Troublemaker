using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='OfficeContract']")]
    public sealed class DialogActionOfficeContract : DialogAction, IMessageHandler
    {
        [XPath("@Title")] public String Title;
        [XPath("@C_OfficeName")] public String OfficeName;
        [XPath("@Target")] public String Target;

        public TextReference TitleId { get; private set; }

        public override void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog)
        {
            if (tree.TryGet(nameof(Title), out var title))
                TitleId = title.Value;
        }

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return (nameof(Title), TitleId, null);
        }
    }
}