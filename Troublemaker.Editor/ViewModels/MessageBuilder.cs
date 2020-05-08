using System;
using System.Collections.Generic;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class MessageBuilder
    {
        public Int32 Level { get; }
        public  String Name { get; }

        private readonly LinkedList<StageMessage> _messages = new LinkedList<StageMessage>();
        private readonly LinkedList<MessageBuilder> _children = new LinkedList<MessageBuilder>();
        private Boolean CanFlatten { get; set; }

        public MessageBuilder(Int32 level, String name)
        {
            Level = level;
            Name = name;
        }
        
        public MessageBuilder Child(String name, IExpandable? expandable)
        {
            var child = new MessageBuilder(Level + 1, name) {CanFlatten = expandable.CanFlatten};
            _children.AddLast(child);
            return child;
        }

        public void AddMessage(IMessageHandler messageHandler, IStage stage)
        {
            foreach ((String name, TextReference key, StageSpeakerInfo? speakerHandler) in messageHandler.EnumerateMessageKeys(stage))
            {
                if (key == default)
                    continue;
                
                if (!Localize.HasKey(key))
                    continue;

                String speakerName = String.IsNullOrEmpty(speakerHandler?.Name) ? name : speakerHandler.Name;
                if (messageHandler is StageActionBalloonChat)
                    speakerName = $"{speakerName} (floating)";

                _messages.AddLast(new StageMessage(speakerName, key) {Speaker = speakerHandler});
            }
        }

        public Boolean TryBuild(String parentName, out IStageMessage? message)
        {
            List<IStageMessage> list = new List<IStageMessage>();
            list.AddRange(_messages);

            foreach (var child in _children)
            {
                if (child.TryBuild(Name, out var childMessage))
                    list.Add(childMessage);
            }

            if (list.Count == 0)
            {
                message = null;
                return false;
            }

            if (list.Count == 1 && CanFlatten)
            {
                message = list[0];
                return true;
            }

            message = new StageMessageGroup(parentName, list.ToArray());
            return true;
        }
    }
}