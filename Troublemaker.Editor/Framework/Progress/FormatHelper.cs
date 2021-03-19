using System;

namespace Troublemaker.Editor.Framework
{
    public static class FormatHelper
    {
        public static String BytesFormat(Double value)
        {
            Int32 i = 0;
            while ((value > 1024))
            {
                value /= 1024;
                i++;
            }

            switch (i)
            {
                case 0:
                    return $"{value:F2} Byte";
                case 1:
                    return $"{value:F2} KByte";
                case 2:
                    return $"{value:F2} MByte";
                case 3:
                    return $"{value:F2} GByte";
                case 4:
                    return $"{value:F2} TByte";
                case 5:
                    return $"{value:F2} PByte";
                case 6:
                    return $"{value:F2} EByte";
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}