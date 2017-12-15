using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Aquarium.UI
{
    class ImageLoaderFromFile : IImageLoader
    {
        private List<Bitmap> images;

        public ImageLoaderFromFile(string gameObject)
        {
            ChangeObject(gameObject);
        }

        public void ChangeObject(string gameObject)
        {
            var dir = new DirectoryInfo("Resources");
            images = dir
                .EnumerateFiles(gameObject + "*.png")
                .Select(file => (Bitmap)Image.FromFile(file.FullName))
                .ToList();
        }

        public List<Bitmap> GetImages()
        {
            return images;
        }
    }
}