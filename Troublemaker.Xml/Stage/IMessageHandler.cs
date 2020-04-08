using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public interface IMessageHandler : IExpandable
    {
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage);
    }
}