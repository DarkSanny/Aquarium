using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Aquarium.UI
{
    class ImageSource : IImage
    {
        private List<Bitmap> images;

        public ImageSource(string gameObject)
        {
            List<Bitmap> images = new List<Bitmap>();
            DirectoryInfo dir = new DirectoryInfo("Resources");
            foreach (FileInfo file in dir.EnumerateFiles(gameObject + "*.png"))
            {
                images.Add((Bitmap)Image.FromFile(file.FullName));
            }
        }

        public List<Bitmap> GetImages()
        {
            return images;
        }
    }
}
