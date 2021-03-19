// using System;
// using System.Collections.Generic;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public class StageMissionDirectsViewModel : IStageComponentViewModel
//     {
//         private readonly Stage _stage;
//
//         public StageMissionDirectsViewModel(Stage stage)
//         {
//             _stage = stage;
//         }
//
//         public override String Name => "MissionDirects";
//
//         protected override IEnumerable<IStageComponentViewModel> EnumerateComponents()
//         {
//             foreach (var item in _stage.MissionDirects)
//             {
//                 IStageComponentViewModel? component = TryWrap(item);
//                 if (component != null)
//                     yield return component;
//             }
//         }
//
//         private IStageComponentViewModel? TryWrap(StageMissionDirect missionDirect)
//         {
//             StageMissionDirectViewModel model = new StageMissionDirectViewModel(_stage, missionDirect);
//             if (model.HasMessages)
//                 return model;
//
//             return null;
//         }
//     }
// }