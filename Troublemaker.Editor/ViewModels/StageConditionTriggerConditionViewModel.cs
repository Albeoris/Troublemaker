// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageConditionTriggerConditionViewModel : IStageComponentViewModel
//     {
//         private readonly StageConditionTriggerCondition _objective;
//
//         public StageConditionTriggerConditionViewModel(StageConditionTriggerCondition objective)
//         {
//             _objective = objective;
//         }
//
//         public override String Name => _objective.Key;
//
//         protected override IEnumerable<IStageMessage> EnumerateMessages()
//         {
//             if (!String.IsNullOrEmpty(_objective.TitleId))
//                 yield return new StageMessage("Title", _objective.TitleId);
//         }
//     }
// }