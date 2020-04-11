using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Selection']")]
    public sealed class DialogActionSelection : DialogAction, IMessageHandler
    {
        [XPath("@C_DlgName")] public String DlgName;
        [XPath("@Content")] public String Content;
        [XPath("@C_Price")] public String Price;
        [XPath("@Target")] public String Target;
        
        [XPath("property")] public DialogSelection[]? Selection;

        public TextReference ContentId { get; private set; }

        private StageSpeakerInfo? _speaker;
        
        public override void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog)
        {
            _speaker = dialog.TryResolveNpcName(DlgName);
            
            if (tree.TryGet(nameof(Content), out var content))
                ContentId = content.Value;

            if (!(Selection?.Length > 0))
                return;
            
            for (Int32 i = 0; i < Selection.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Selection[i].Translate(child);
            }
        }

        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            if (Selection is null)
                yield break;

            for (var index = 0; index < Selection.Length; index++)
            {
                var section = Selection[index];
                yield return ($"Selection {index}", section);
            }
        }

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            String name = _speaker?.Name ?? DlgName;

            yield return ($"{name}: {nameof(Content)}", ContentId, _speaker);
        }
    }
}