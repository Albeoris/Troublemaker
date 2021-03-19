using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='UpdateFriendship']")]
    public sealed class DialogActioUpdateFriendship : DialogAction
    {
        [XPath("@FriendshipType")] public String FriendshipType;
        [XPath("@FriendshipName")] public String FriendshipName;
        [XPath("@C_FriendshipPoint")] public String FriendshipPoint;
    }
}