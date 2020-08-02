using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspaces")]
    public sealed class DbMasteries
    {
        [XPath("idspace[@id='MasteryType']/class/@name")]
        public Map<XmlMasteryType> Types;
        
        [XPath("idspace[@id='MasteryType']/schema/rule/@property")]
        public Map<XmlRule> TypesSchema;

        [XPath("idspace[@id='MasteryUnlockLevel']/class/@name")]
        public Map<XmlMasteryUnlockLevel> UnlockLevels;

        [XPath("idspace[@id='FatigueMastery']/class/@name")]
        public Map<XmlMasteryFatigue> Fatigue;

        [XPath("idspace[@id='MasteryAbilityType']/class/@name")]
        public Map<XmlMasteryAbilityType> AbilityTypes;

        [XPath("idspace[@id='MasteryCategory']/class/@name")]
        public Map<XmlMasteryCategory> Categories;
        
        [XPath("idspace[@id='MasteryCategory']/schema/rule/@property")]
        public Map<XmlRule> CategoriesSchema;

        [XPath("idspace[@id='Mastery']/class/@name")]
        public Map<XmlMastery> Entries;

        [XPath("idspace[@id='Mastery']/schema/rule/@property")]
        public Map<XmlRule> EntriesSchema;
    }
}