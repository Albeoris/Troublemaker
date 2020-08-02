using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public interface IXmlObject
    {
        public Map<String> Attributes { get; }
    }
    
    [XPath("self::class")]
    public sealed class XmlMastery : IXmlObject, IExpandable, IMessageHandler
    {
        [XPath("@name")] public String Name; // "Bonecrusher"
        [XPath("@Base_Title")] public String Base_Title; // "살을 주고 뼈를 취한다."
        [XPath("@Edible")] public Boolean Edible; // "false"
        [XPath("@Life")] public Boolean Life; // "false"
        [XPath("@IsEnableESP")] public Boolean IsEnableESP; // "false"
        [XPath("@Category")] public String Category; // "Set"
        [XPath("@Type")] public String Type; // "Swordsman"
        [XPath("@MaxLv")] public Int64 MaxLv; // "1"
        [XPath("@Cost")] public Int64 Cost; // "0"
        [XPath("@MasterRate")] public Int64 MasterRate; // "0"
        [XPath("@FlavorText")] public String FlavorText; // "작은 손실을 두려워하지 않는다."
        [XPath("@ExtractItem")] public String ExtractItem; // "None"
        [XPath("@UseSubMasteryTooltip")] public Boolean UseSubMasteryTooltip; // "false"
        [XPath("@UseBuffTooltip")] public Boolean UseBuffTooltip; // "true"
        [XPath("@Mastery")] public String Mastery; // "None"
        [XPath("@Ability")] public String Ability; // "None"
        [XPath("@ChainAbility")] public String ChainAbility; // "None"
        [XPath("@ModifyAbility")] public String ModifyAbility; // "None"
        [XPath("@ModifyAbilityType")] public String ModifyAbilityType; // "None"
        [XPath("@ModifyAbilityOrder")] public Int64 ModifyAbilityOrder; // "0"
        [XPath("@Range")] public String Range; // "None"
        [XPath("@SubRange1")] public String SubRange1; // "None"
        [XPath("@SubRange2")] public String SubRange2; // "None"
        [XPath("@TurnPlayType")] public String TurnPlayType; // "None"
        [XPath("@SubType")] public String SubType; // "None"
        [XPath("@Buff")] public String Buff; // "None"
        [XPath("@SubBuff")] public String SubBuff; // "None"
        [XPath("@ThirdBuff")] public String ThirdBuff; // "None"
        [XPath("@ForthBuff")] public String ForthBuff; // "None"
        [XPath("@ApplyAmountType")] public String ApplyAmountType; // "Percent"
        [XPath("@ApplyAmountType2")] public String ApplyAmountType2; // "None"
        [XPath("@ApplyAmountType3")] public String ApplyAmountType3; // "None"
        [XPath("@ApplyAmountType4")] public String ApplyAmountType4; // "None"
        [XPath("@ApplyAmountType5")] public String ApplyAmountType5; // "None"
        [XPath("@CounterMeleeAttack")] public Boolean CounterMeleeAttack; // "false"
        [XPath("@CounterShooting")] public Boolean CounterShooting; // "false"
        [XPath("@ReactionMeleeAttack")] public Boolean ReactionMeleeAttack; // "false"
        [XPath("@ReactionShooting")] public Boolean ReactionShooting; // "false"

        [XPath("Desc_Base/property")] public XmlFormatText[] Args;
        [XPath("@*")] public Map<String> Attributes { get; set; }

        public TextReference TitleId;
        public TextReference FlavorTextId;

        public void Translate(LocalizationTree tree)
        {
            if (!tree.TryGet(Name, out tree))
                return;

            LocalizationTree child;

            if (tree.TryGet(nameof(Base_Title), out child))
                TitleId = child.Value;

            if (tree.TryGet(nameof(FlavorText), out child))
                FlavorTextId = child.Value;

            if (tree.TryGet("Desc_Base", out child))
            {
                for (Int32 i = 0; i < Args.Length; i++)
                {
                    if (child.TryGet(i, out var value))
                        Args[i].Translate(value);
                }
            }
        }

        public String NodeName => Name;
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TitleId, null);
            for (var index = 0; index < Args.Length; index++)
            {
                String prefix = $"Line {index + 1}";
                XmlFormatText line = Args[index];
                yield return (prefix, line.TextId, null);
                yield return (prefix + ": Case", line.CaseValueId, null);
                foreach (var arg in line.Args)
                    yield return (prefix + $": ${arg.FormatKey}$", arg.ValueId, null);
            }

            yield return ("Flavor", FlavorTextId, null);
        }
    }
}