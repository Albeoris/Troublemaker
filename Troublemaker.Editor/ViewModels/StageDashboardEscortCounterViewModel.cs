// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageDashboardEscortCounterViewModel : IStageComponentViewModel
//     {
//         private readonly StageDashboardEscortCounter _dashboard;
//
//         public StageDashboardEscortCounterViewModel(StageDashboardEscortCounter dashboard)
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