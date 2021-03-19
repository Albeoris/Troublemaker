// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageObjectivesViewModel : IStageComponentViewModel
//     {
//         private readonly Stage _stage;
//
//         public StageObjectivesViewModel(Stage stage)
//         {
//             _stage = stage;
//         }
//
//         public override String Name => "Objectives";
//
//         protected override IEnumerable<IStageComponentViewModel> EnumerateComponents()
//         {
//             foreach (var item in _stage.Objectives)
//             {
//                 IStageComponentViewModel? component = TryWrap(item);
//                 if (component != null)
//                     yield return component;
//             }
//         }
//
//         private static IStageComponentViewModel? TryWrap(StageCondition item)
//         {
//             switch (item)
//             {
//                 case StageConditionTriggerCondition triggerCondition:
//                     return new StageConditionTriggerConditionViewModel(triggerCondition);
//                 case StageConditionDashboardEvaluator dashboardEvaluator:
//                     return new StageConditionDashboardEvaluatorViewModel(dashboardEvaluator);
//                 default:
//                     return default;
//             }
//         }
//     }
// }