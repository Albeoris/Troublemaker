using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Pages
{
    public sealed class PortraitSet
    {
        private readonly Dictionary<String, Portraits> _dic;
        
        public static PortraitSet Instance { get; } = new PortraitSet();

        public PortraitSet()
        {
            DbObjects objects = DB.Objects;
            _dic = new Dictionary<String, Portraits>(objects.Items.Count);

            foreach (var obj in objects.Items)
            {
                Portraits portraits = new Portraits(GetPortertSource(obj.Image), GetPortertSource(obj.Image_Small));
                _dic.Add(obj.Name, portraits);

                foreach (var emotion in obj.Emotions)
                {
                    Emotion em = new Emotion(GetPortertSource(emotion.Image), GetPortertSource(emotion.Icon));
                    portraits.Emotions.Add(emotion.Name, em);
                }
            }
        }

        private BitmapSource? GetPortertSource(String imageName)
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

        public BitmapSource? FindImage(String objectName, String emotionName)
        {
            if (objectName == null)
                return null;

            if (_dic.TryGetValue(objectName, out var portraits))
            {
                if (emotionName != null && portraits.Emotions.TryGetValue(emotionName, out var emotion))
                    return emotion.Image ?? portraits.DefaultImage;
                
                if (portraits.DefaultImage == null && portraits.Emotions.TryGetValue("Normal", out emotion))
                    return emotion.Image;

                return portraits.DefaultImage;
            }

            return null;
        }

        public BitmapSource? FindIcon(String objectName, String emotionName)
        {
            if (objectName == null)
                return null;

            if (_dic.TryGetValue(objectName, out var portraits))
            {
                if (emotionName != null && portraits.Emotions.TryGetValue(emotionName, out var emotion))
                    return emotion.Icon ?? portraits.DefaultIcon;

                if (portraits.DefaultIcon == null && portraits.Emotions.TryGetValue("Normal", out emotion))
                    return emotion.Icon;

                return portraits.DefaultIcon;
            }

            return null;
        }

        private sealed class Portraits
        {
            public BitmapSource? DefaultImage { get; }
            public BitmapSource? DefaultIcon { get; }
            public Dictionary<String, Emotion> Emotions { get; } = new Dictionary<String, Emotion>();

            public Portraits(BitmapSource? image, BitmapSource? icon)
            {
                DefaultImage = image;
                DefaultIcon = icon;
            }
        }

        private sealed class Emotion
        {
            public BitmapSource? Image { get; set; }
            public BitmapSource? Icon { get; set; }

            public Emotion(BitmapSource? image, BitmapSource? icon)
            {
                Image = image;
                Icon = icon;
            }
        }
    }
}