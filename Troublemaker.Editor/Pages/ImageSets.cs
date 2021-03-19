using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public sealed class ImageSets
    {
        public static ImageSets Instance { get; } = new ImageSets();

        private readonly Dictionary<String, ImageSet> _imageSets = new Dictionary<String, ImageSet>();

        public ImageSet Get(String setName)
        {
            if (!_imageSets.TryGetValue(setName, out var imageSet))
            {
                String imagePath = $@"Data/CEGUI/datafiles/imagesets/{setName}.imageset";
                imageSet = new ImageSet(imagePath);
                _imageSets.Add(setName, imageSet);
            }

            return imageSet;
        }
        
        public static BitmapSource? FindImageSource(String imageName)
        {
            if (String.IsNullOrEmpty(imageName) || imageName == "None")
                return null;

            String[] parts = imageName.Split('/');
            if (parts.Length == 2)
            {
                String setName = parts[0];
                String portraitName = parts[1];

                return ImageSets.Instance.Get(setName)[portraitName];
            }
            
            if (parts.Length == 1)
            {
                var path = ImagePaths.FindPath(parts[0]);
                if (path == null)
                    return null;

                return new BitmapImage(new Uri(path, UriKind.Absolute));
            }

            throw new NotSupportedException(imageName);
        }
    }
}