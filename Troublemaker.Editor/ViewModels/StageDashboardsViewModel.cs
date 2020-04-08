// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageDashboardsViewModel : IStageComponentViewModel
//     {
//         private readonly Stage _stage;
//
//         public StageDashboardsViewModel(Stage stage)
//         {
//             _stage = stage;
//         }
//
//         public override String Name => "Dashboards";
//
//         protected override IEnumerable<IStageComponentViewModel> EnumerateComponents()
//         {
//             foreach (var item in _stage.Dashboards)
//             {
//                 IStageComponentViewModel? component = TryWrap(item);
//                 if (component != null)
//                     yield return component;
//             }
//         }
//
//         private static IStageComponentViewModel? TryWrap(StageDashboard item)
//         {
//             switch (item)
//             {
//                 case StageDashboardCounter counter:
//                     return new StageDashboardCounterViewModel(counter);
//                 case  StageDashboardEscortCounter escortCounter:
//                     return new StageDashboardEscortCounterViewModel(escortCounter);
//                 case StageDashboardTimeLimiter timeLimiter:
//                     return new StageDashboardTimeLimiterViewModel(timeLimiter);
//                 case StageDashboardObjectiveMarker objectiveMarker:
//                     return new StageDashboardObjectiveMarkerViewModel(objectiveMarker);
//                 case  StageDashboardChecklist checklist:
//                     return new StageDashboardChecklistViewModel(checklist);
//                 case  StageDashboardHitList hitList:
//                     return new StageDashboardHitListViewModel(hitList);
//                 default:
//                     return default;
//             }
//         }
//     }
// }