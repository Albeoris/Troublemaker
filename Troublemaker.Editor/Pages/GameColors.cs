using System;
using System.Windows.Media;
using Troublemaker.Framework;

namespace Troublemaker.Editor.Pages
{
    public static class GameColors
    {
        public static Map<Color> KnownColors { get; }

        static GameColors()
        {
            Map<Color> knownColors = new Map<Color>(_knownColors.Length);
            foreach (var number in _knownColors)
            {
                var a = (Byte) ((number >> 24) & 0xFF);
                var r = (Byte) ((number >> 16) & 0xFF);
                var g = (Byte) ((number >> 8) & 0xFF);
                var b = (Byte) (number & 0xFF);

                var color = Color.FromArgb(a, r, g, b);
                var key = number.ToString("X8");
                knownColors.Add(key, color);
            }

            KnownColors = knownColors;
        }

        private static readonly UInt32[] _knownColors =
        {
            0xFF0000FF,
            0xFF0070FF,
            0xFF00AAFF,
            0xFF00C8FF,
            0xFF00FF00,
            0xFF00FFFF,
            0xFF1EFF00,
            0xFF1EFFFF,
            0xFF22FF22,
            0xFF22FFFF,
            0xFF32CD32,
            0xFF55FF55,
            0xFF636564,
            0xFF6699CC,
            0xFF9D9D9D,
            0xFFC0C0C0,
            0xFFCC55EE,
            0xFFCCCCCC,
            0xFFCD7F32,
            0xFFDB00D3,
            0xFFF3B0A8,
            0xFFF3C993,
            0xFFFF0000,
            0xFFFF186F,
            0xFFFF5500,
            0xFFFF5943,
            0xFFFF6D25,
            0xFFFF8000,
            0xFFFF99FF,
            0xFFFFAA00,
            0xFFFFAA55,
            0xFFFFD700,
            0xFFFFE959,
            0xFFFFFF00,
            0xFFFFFF55,
            0xFFFFFF59,
            0xFFFFFF88,
            0xFFFFFFAA,
            0xFFFFFFC8,
            0xFFFFFFFF
        };
    }
}