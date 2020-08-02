using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasteryCategory : IXmlObject
    {
        [XPath("@name")] public String Name;                              // "ComplementaryModule"
        [XPath("@Type")] public String Type;                              // "Normal"
        [XPath("@Order")] public Int64 Order;                             // "4"
        [XPath("@IsMasteryBoard")] public Boolean IsMasteryBoard;         // "true"
        [XPath("@IsMasteryInvnetory")] public Boolean IsMasteryInvnetory; // "true"
        [XPath("@IsTechnique")] public Boolean IsTechnique;               // "true"
        [XPath("@IsMachine")] public Boolean IsMachine;                   // "true"
        [XPath("@EquipSlot")] public String EquipSlot;                    // "Attack"
        [XPath("@EnableRace")] public String EnableRace;                  // "Machine"
        [XPath("@Prob")] public Int64 Prob;                               // "0"
        [XPath("@Image")] public String Image;                            // "Icons/ComplementaryModule"
        [XPath("@PushedImage")] public String PushedImage;                // "Icons/ComplementaryModule_Pushed"
        [XPath("@HoverImage")] public String HoverImage;                  // "Icons/ComplementaryModule_Hover"
        [XPath("@Color")] public String Color;                            // "Combine_Red_Flat"
        [XPath("@Title")] public String Title;                            // "강화"
        [XPath("@Title_Menu")] public String TitleMenu;                   // "강화 모듈"
        [XPath("@Desc")] public String Desc;                              // "강화 모듈"
        
        [XPath("@*")] public Map<String> Attributes { get; set; }
    }
}