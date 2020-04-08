using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public sealed class ImageSet
    {
        private readonly ImageSetV2 _imageSet;
        private readonly BitmapImage _fullBitmap;
        private readonly Dictionary<String, BitmapSource?> _sources;

        public ImageSet(String imageSetPath)
        {
            _imageSet = XmlDeserializerFactory.Default.Deserialize<ImageSetV2>(imageSetPath);

            var directory = Path.GetDirectoryName(imageSetPath) ?? throw new NotSupportedException(imageSetPath);
            var bitmapPath = Path.GetFullPath(Path.Combine(directory, _imageSet.ImageFileName));

            _fullBitmap = new BitmapImage(new Uri(bitmapPath, UriKind.Absolute));
            _sources = new Dictionary<String, BitmapSource?>(_imageSet.Images.Count);
        }

        public BitmapSource? this[String? name]
        {
            get
            {
                if (name == null)
                    return null;

                if (_sources.TryGetValue(name, out var imageSource))
                    return imageSource;

                if (!_imageSet.Images.TryGetValue(name, out var desc))
                {
                    _sources.Add(name, null);
                    return null;
                }

                Int32Rect sourceRect = new Int32Rect(desc.X, desc.Y, desc.Width, desc.Height);
                imageSource = new CroppedBitmap(_fullBitmap, sourceRect);
                _sources.Add(desc.Name, imageSource);

                return imageSource;
            }
        }
    }
}