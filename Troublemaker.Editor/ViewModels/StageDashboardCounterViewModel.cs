// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageDashboardCounterViewModel : IStageComponentViewModel
//     {
//         private readonly StageDashboardCounter _dashboard;
//
//         public StageDashboardCounterViewModel(StageDashboardCounter dashboard)
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