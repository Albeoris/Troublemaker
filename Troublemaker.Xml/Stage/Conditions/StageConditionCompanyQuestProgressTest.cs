using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='CompanyQuestProgressTest']")]
    public sealed class StageConditionCompanyQuestProgressTest : StageCondition
    {
        [XPath("@Quest")] public String Quest;
    }
}