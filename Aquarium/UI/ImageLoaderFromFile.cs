using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Aquarium.UI
{
    public class ImageLoaderFromFile : IImageLoader
    {
        private List<Bitmap> _images;

        public ImageLoaderFromFile(string gameObject)
        {
            ChangeObject(gameObject);
        }

        public void ChangeObject(string gameObject)
        {
            var dir = new DirectoryInfo("Resources");
            _images = dir
                .EnumerateFiles(gameObject + "*.png")
				.OrderBy(file => int.Parse(Path.GetFileNameWithoutExtension(file.FullName).Substring(gameObject.Length)))
                .Select(file => (Bitmap)Image.FromFile(file.FullName))
                .ToList();
        }

        public List<Bitmap> GetImages()
        {
            return _images;
        }
    }
}