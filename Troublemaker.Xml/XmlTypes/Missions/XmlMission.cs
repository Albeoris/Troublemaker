using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMission
    {
        [XPath("@name")] public String name;                                        // "Tutorial_CrowBill"
        [XPath("@Stage")] public String Stage;                                      // "Tutorial_CrowBill.stage"
        [XPath("@ProgressOrder")] public Int64 ProgressOrder;                       // "1"
        [XPath("@Lv")] public Int64 Lv;                                             // "1"
        [XPath("@LocationTitle")] public String LocationTitle;                      // "까마귀부리 정류장"
        [XPath("@Zone")] public String Zone;                                        // "WindWallTown" 
        [XPath("@Slot")] public String Slot;                                        // "CrowStreet_Street" 
        [XPath("@Difficulty")] public String Difficulty;                            // "Difficulty1" 
        [XPath("@EnemyGradeUpMinCount")] public Int64 EnemyGradeUpMinCount;         // "0" 
        [XPath("@EnemyGradeUpMaxCount")] public Int64 EnemyGradeUpMaxCount;         // "0" 
        [XPath("@EnableEnemyGradeUp")] public String EnableEnemyGradeUp;            // "false" 
        [XPath("@IsFirstLook")] public String IsFirstLook;                          // "false" 
        [XPath("@Type")] public String Type;                                        // "CivilRescue" 
        [XPath("@SpotlightObject")] public String SpotlightObject;                  // "None" 
        [XPath("@StartBGM")] public String StartBGM;                                // "Infiltration" 
        [XPath("@BattleBGM")] public String BattleBGM;                              // "BeginningBattle" 
        [XPath("@TargetTime")] public Int64 TargetTime;                             // "500" 
        [XPath("@TargetKillCount")] public Int64 TargetKillCount;                   // "4" 
        [XPath("@TargetRescueCount")] public Int64 TargetRescueCount;               // "2" 
        [XPath("@MissionTime")] public String MissionTime;                          // "Night" 
        [XPath("@Weather")] public String Weather;                                  // "Rain" 
        [XPath("@Temperature")] public String Temperature;                          // "Normal" 
        [XPath("@LocationImage")] public String LocationImage;                      // "" 
        [XPath("@FixedMember")] public String FixedMember;                          // "" 
        [XPath("@HiddenMember")] public String HiddenMember;                        // "" 
        [XPath("@BanMember")] public String BanMember;                              // "" 
        [XPath("@FreeMemberCount")] public Int64 FreeMemberCount;                   // "0" 
        [XPath("@StageKey")] public String StageKey;                                // "Normal" 
        [XPath("@Loading")] public String Loading;                                  // "Mission_TutorialCrowBill" 
        [XPath("@MinMemberCount")] public Int64 MinMemberCount;                     // "0" 
        [XPath("@SystemMenuType")] public String SystemMenuType;                    // "Tutorial" 
        [XPath("@Tutorial")] public String Tutorial;                                // "true" 
        [XPath("@AssistProgress")] public String AssistProgress;                    // "InProgress" 
        [XPath("@AutoCivilRescueAll")] public String AutoCivilRescueAll;            // "false" 
        [XPath("@EnableCivilRescueReward")] public String EnableCivilRescueReward;  // "false" 
        [XPath("@BeginFadeOut")] public String BeginFadeOut;                        // "true" 
        [XPath("@PsionicStoneType")] public String PsionicStoneType;                // "" 
        [XPath("@PsionicStoneAmount")] public String PsionicStoneAmount;            // "None"
    }
}