using System;
using System.Collections;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public interface IMessageHandler : IExpandable
    {
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage);
    }

    public interface IStage
    {
        Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent);
        IEnumerable<(string name, IExpandable expandable)> EnumerateChildren();
    }
}