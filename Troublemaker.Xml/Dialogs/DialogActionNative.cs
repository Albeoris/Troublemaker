using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[not(@Type)]")]
    public sealed class DialogActionNative : DialogAction, IMessageHandler
    {
        [XPath("@C_DlgName")] public String DlgName;
        [XPath("property/@Text")] public String[] FormatLines;

        public TextReference[] FormatLineIds { get; private set; } = Array.Empty<TextReference>();

        private Dialog _dialog;
        
        public override void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog)
        {
            _dialog = dialog;
            
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
            StageSpeakerInfo? speaker = _dialog.TryResolveNpcName(DlgName);
            String name = speaker?.Name ?? DlgName;
            
            for (var index = 0; index < FormatLineIds.Length; index++)
            {
                TextReference line = FormatLineIds[index];
                yield return ($"{name} {index}", line, speaker);
            }
        }
    }
}