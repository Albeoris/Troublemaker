using System;
using System.Collections.Generic;
using System.Linq;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Dialog']")]
    public sealed class DialogActionDialog : DialogAction, IMessageHandler
    {
        [XPath("@C_DlgName")] public String DlgName;
        [XPath("property/@Text")] public String[] Lines;
        
        public TextReference[] LineIds { get; private set; } = Array.Empty<TextReference>();

        private Dialog _dialog;
        
        public override void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog)
        {
            _dialog = dialog;
            
            if (!(Lines?.Length > 0))
                return;
            
            LineIds = new TextReference[Lines.Length];
            for (Int32 i = 0; i < LineIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    LineIds[i] = child["Text"].Value;
            }
        }

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            StageSpeakerInfo? speaker = _dialog.TryResolveNpcName(DlgName);
            String name = speaker?.Name ?? DlgName;
            for (var index = 0; index < LineIds.Length; index++)
            {
                TextReference line = LineIds[index];
                yield return ($"{name} {index}", line, speaker);
            }
        }
    }
}