using System;
using System.Collections.Generic;

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
    }
}