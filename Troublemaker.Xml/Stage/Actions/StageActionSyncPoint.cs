using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SyncPoint']")]
    public sealed class StageActionSyncPoint : StageAction
    {
    }
}