using System;
using  System.Collections.Generic;
using System.Linq;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::idspace[@id='Dialog']")]
    public sealed class Dialog : IStage
    {
        [XPath("class")] public DialogScript[] Scripts { get; set; }

        public StageSpeaker NpcSpeaker { get; private set; }

        public void Translate(LocalizationTree tree)
        {
            NpcSpeaker = ResolveSpeaker();

            tree = tree["Dialog"];
            foreach (var script in Scripts)
            {
                if (tree.TryGet(script.Name, out var child))
                    script.Translate(child, this);
            }
        }

        private StageSpeaker ResolveSpeaker()
        {
            String? mainName = Scripts?.FirstOrDefault()?.Name;
            XmlNpc? npc = DB.Npc.FindByDialogName(mainName);
            if (npc is null)
                return null;

            StageSpeaker result = new StageSpeaker();
            result.ImagePath = npc.MarkImage;

            String infoKey = npc.Info;
            if (String.IsNullOrEmpty(infoKey) || infoKey == "Empty")
                result.Info = npc.Name;
            else
                result.Info = infoKey;

            return result;
        }

        public Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent) => throw new NotSupportedException();

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Scripts.Named(nameof(Scripts));
        }

        public StageSpeakerInfo? TryResolveNpcName(String dlgName)
        {
            if (NpcSpeaker is null)
                return null;
            
            return dlgName == "npc.Info.Title" ? new StageSpeakerInfo(NpcSpeaker) : null;
        }
    }
}