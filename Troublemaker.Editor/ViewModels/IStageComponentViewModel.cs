// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Windows.Controls;
// using Troublemaker.Xml;
//
// namespace Troublemaker.Editor.ViewModels
// {
//     public abstract class IStageComponentViewModel
//     {
//         public abstract String Name { get; }
//
//         public IEnumerable<IStageComponentViewModel> Components => EnumerateComponents();
//         public StageMessageGroup Messages
//         {
//             get
//             {
//                 List<IStageMessage> messages = new List<IStageMessage>();
//                 
//                 foreach (IStageMessage message in  EnumerateMessages())
//                 {
//                     switch (message)
//                     {
//                         case StageMessage sm:
//                         {
//                             if (Localize.All.Any(l => l.HasKey(sm.Key)))
//                                 messages.Add(sm);
//                             break;
//                         }
//                         case StageMessageGroup smg:
//                         {
//                             messages.Add(smg);
//                             break;
//                         }
//                         default:
//                         {
//                             throw new NotSupportedException(message.GetType().FullName);
//                         }
//                     }
//                 }
//
//                 return new StageMessageGroup(Name, messages.ToArray()) {IsScrollable = ScrollBarVisibility.Visible};
//             }
//         }
//
//         protected virtual IEnumerable<IStageComponentViewModel> EnumerateComponents() => Array.Empty<IStageComponentViewModel>();
//         protected virtual IEnumerable<IStageMessage> EnumerateMessages() => Array.Empty<IStageMessage>();
//     }
// }