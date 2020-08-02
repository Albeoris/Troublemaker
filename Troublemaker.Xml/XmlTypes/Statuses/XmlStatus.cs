using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlStatus : IXmlObject, IExpandable, IMessageHandler
    {
        [XPath("@name")] public String Name; // "RegenVigor"
        [XPath("@DamageColor")] public String DamageColor; // "NavyBlue"
        [XPath("@ItemUpgradeType")] public String ItemUpgradeType; // "Spirit"
        [XPath("@Priority")] public Int64 Priority; // "7"
        [XPath("@OptionRatio")] public Double OptionRatio; // "0.98"
        [XPath("@Title")] public String Title; // "NavyBlue"
        [XPath("@Title_HPChangeFunctionArg")] public String Title_HPChangeFunctionArg; // "기력 회복"
        [XPath("@Type")] public String Type; // "Main"
        [XPath("@Title_Short")] public String Title_Short; // "기력 회복"
        [XPath("@Desc_Increase")] public String Desc_Increase; // "$Status$[이] $Value$ 증가합니다."
        [XPath("@Desc_Decrease")] public String Desc_Decrease; // "$Status$[이] $Value$ 감소합니다."
        [XPath("@Desc_IncreaseByLevel")] public String Desc_IncreaseByLevel; // "레벨 당 $Status$[이] $Value$ 증가합니다."
        [XPath("@Desc_DecreaseByLevel")] public String Desc_DecreaseByLevel; // "레벨 당 $Status$[이] $Value$ 감소합니다."
        [XPath("@Desc_Format")] public String Desc_Format; // "$Status$[은] 매 턴 시작 시 회복하는 기력량입니다."
        [XPath("@LimitType")] public String LimitType; // "Int"
        [XPath("@Format")] public String Format; // "Int"
        
        [XPath("@*")] public Map<String> Attributes { get; set; }

        public TextReference TitleId;
        public TextReference TitleShortId;
        public TextReference TitleHPChangeFunctionArgId;
        public TextReference DescIncreaseId;
        public TextReference DescIncreaseByLevelId;
        public TextReference DescDecreaseId;
        public TextReference DescDecreaseByLevelId;

        public void Translate(LocalizationTree tree)
        {
            if (!tree.TryGet(Name, out tree))
                return;

            LocalizationTree child;

            if (tree.TryGet(nameof(Title), out child))
                TitleId = child.Value;

            if (tree.TryGet(nameof(Title_Short), out child))
                TitleShortId = child.Value;

            if (tree.TryGet(nameof(Title_HPChangeFunctionArg), out child))
                TitleHPChangeFunctionArgId = child.Value;

            if (tree.TryGet(nameof(Desc_Increase), out child))
                DescIncreaseId = child.Value;
            if (tree.TryGet(nameof(Desc_IncreaseByLevel), out child))
                DescIncreaseByLevelId = child.Value;

            if (tree.TryGet(nameof(Desc_Decrease), out child))
                DescDecreaseId = child.Value;
            if (tree.TryGet(nameof(Desc_DecreaseByLevel), out child))
                DescDecreaseByLevelId = child.Value;
        }

        public String NodeName => Name;
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TitleId, null);
            yield return ("Title Short", TitleShortId, null);
            yield return ("Title HP Change", TitleHPChangeFunctionArgId, null);
            yield return ("Description Increase", DescIncreaseId, null);
            yield return ("Description Increase by Level", DescIncreaseByLevelId, null);
            yield return ("Description Decrease", DescDecreaseId, null);
            yield return ("Description Decrease by Level", DescDecreaseByLevelId, null);
        }
    }
}