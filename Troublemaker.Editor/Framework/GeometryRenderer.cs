using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Troublemaker.Framework;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Windows.FontStyle;
using Point = System.Windows.Point;

namespace Troublemaker.Editor.Framework
{
    public static class GeometryRenderer
    {
        public static Geometry NormalBalloon { get; } = Factory("💬");
        public static Geometry ShoutBalloon { get; } = Factory("🗯");
        public static Geometry ThinkBalloon { get; } = Factory("💭");
        
        private static Geometry Factory(String text)
        {
            FormattedText t = new FormattedText
            (
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Arial"),
                    new FontStyle(),
                    FontWeights.Bold,
                    new FontStretch()),
                32,
                Brushes.White
            );
            
            var g = t.BuildGeometry(new Point(0, 0));
            
            PathGeometry outline = g.GetOutlinedPathGeometry();
            outline = outline.Clone();
            for (int i = outline.Figures.Count - 1; i >= 0; i--)
            {
                var figure = outline.Figures[i];
                if (figure.Segments.Count <= 1)
                    outline.Figures.RemoveAt(i);
            }

            var f = outline.Figures[0];
            outline.Figures.Clear();
            outline.Figures.Add(f);
            outline.Freeze();
            
            return outline;
        }
    }
}