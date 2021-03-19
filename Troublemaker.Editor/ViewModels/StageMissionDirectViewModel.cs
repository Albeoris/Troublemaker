// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public sealed class StageMissionDirectViewModel : IStageComponentViewModel
//     {
//         private readonly Stage _stage;
//         private readonly StageMissionDirect _missionDirect;
//         private readonly IStageMessage[] _messages;
//
//         public StageMissionDirectViewModel(Stage stage, StageMissionDirect missionDirect)
//         {
//             _stage = stage;
//             _missionDirect = missionDirect;
//             _messages = EnumerateMessagesInternal().ToArray();
//         }
//
//         public override String Name => _missionDirect.Key;
//         public Boolean HasMessages => _messages.Length > 0;
//
//         protected override IEnumerable<IStageMessage> EnumerateMessages() => _messages;
//
//         private IEnumerable<IStageMessage> EnumerateMessagesInternal()
//         {
//             foreach (StageAction action in StageActionHelper.EnumerateActionsRecursive(_missionDirect.Actions))
//             {
//                 foreach (IStageMessage message in StageActionHelper.EnumerateMessages(_stage, action))
//                 {
//                     if (message is null)
//                         continue;
//                     
//                     if (message is StageMessage sm && Localize.All.Any(d => d.HasKey(sm.Key)))
//                         yield return message;
//                     else if (message is StageMessageGroup mg && mg.Messages.Length > 0)
//                         yield return message;
//                 }
//             }
//         }
//     }
// }