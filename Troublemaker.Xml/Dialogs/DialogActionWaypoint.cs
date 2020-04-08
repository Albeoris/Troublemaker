using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Waypoint']")]
    public sealed class DialogActionWaypoint : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}