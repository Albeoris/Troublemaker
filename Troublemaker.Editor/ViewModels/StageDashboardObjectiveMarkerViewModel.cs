// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageDashboardObjectiveMarkerViewModel : IStageComponentViewModel
//     {
//         private readonly StageDashboardObjectiveMarker _dashboard;
//
//         public StageDashboardObjectiveMarkerViewModel(StageDashboardObjectiveMarker dashboard)
//         {
//             _dashboard = dashboard;
//         }
//
//         public override String Name => _dashboard.Key;
//
//         protected override IEnumerable<IStageMessage> EnumerateMessages()
//         {
//             if (!String.IsNullOrEmpty(_dashboard.MessageId))
//                 yield return new StageMessage("Message", _dashboard.MessageId);
//         }
//     }
// }