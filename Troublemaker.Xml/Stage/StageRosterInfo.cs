using System;

namespace Troublemaker.Xml
{
    [XPath("self::RosterInfo")]
    public class StageRosterInfo
    {
        [XPath("@RosterKey")] public String RosterKey;
    }
}