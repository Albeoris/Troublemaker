using System;
using Troublemaker.Xml;

namespace Troublemaker.Editor.ViewModels
{
    public sealed class StageMessage : IStageMessage
    {
        public String Name { get; }
        public TextReference Key { get; }
        public StageSpeakerInfo? Speaker { get; set; }

        public StageMessage(String name, TextReference key)
        {
            if (key.IsEmpty)
                throw new ArgumentNullException(nameof(key));

            Name = name;
            Key = key;
        }
    }
}