using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Troublemaker.Xml
{
    public static class ImagePaths
    {
        private static readonly Dictionary<String, String> _nameToPath;

        public static String? FindPath(String name) => _nameToPath.TryGetValue(name, out var result) ? result : null;

        static ImagePaths()
        {
            var directoryPath = "Data/CEGUI/datafiles/imagesets";
            var imgPath = directoryPath + "/img.xml";
            var imageSet = XmlDeserializerFactory.Default.Deserialize<ImageSetBase>(imgPath);
            var fileToName = new Dictionary<String, List<String>>();
            foreach (var v in imageSet.Images.Values)
            {
                if (!fileToName.TryGetValue(v.File, out var names))
                {
                    names = new List<String>(1);
                    fileToName.Add(v.File, names);
                }

                names.Add(v.Name);
            }
            var nameToPath = new Dictionary<String, String>(fileToName.Count);
            
            foreach (String filePath in Directory.EnumerateFiles(directoryPath, "*", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(filePath);
                if (fileToName.TryGetValue(fileName, out var name))
                {
                    foreach (var n in name)
                        nameToPath.Add(n, Path.GetFullPath(filePath));
                }
            }

            _nameToPath = nameToPath;
        }
    }
}