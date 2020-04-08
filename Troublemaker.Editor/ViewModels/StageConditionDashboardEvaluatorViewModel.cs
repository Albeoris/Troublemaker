// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageConditionDashboardEvaluatorViewModel : IStageComponentViewModel
//     {
//         private readonly StageConditionDashboardEvaluator _objective;
//
//         public StageConditionDashboardEvaluatorViewModel(StageConditionDashboardEvaluator objective)
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