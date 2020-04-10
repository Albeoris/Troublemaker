using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[not(@Type)]")]
    public sealed class DialogActionNative : DialogAction, IMessageHandler
    {
        [XPath("@C_DlgName")] public String DialogType;
        [XPath("property/@Text")] public String[] FormatLines;

        public TextReference[] FormatLineIds { get; private set; } = Array.Empty<TextReference>();

        public override void Translate(LocalizationTree tree)
        {
            if (!(FormatLines?.Length > 0))
                return;
            
            FormatLineIds = new TextReference[FormatLines.Length];
            for (int i = 0; i < FormatLineIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    FormatLineIds[i] = child["Text"].Value;
            }
        }

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            for (var index = 0; index < FormatLineIds.Length; index++)
            {
                TextReference line = FormatLineIds[index];
                yield return ($"Line {index}", line, null);
            }
        }
    }
}