using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    public class ImageSource : IImage
    {
        private readonly List<Bitmap> _sprites;

        public ImageSource(string gameObject)
        {
            var loader = new ImageLoaderFromFile(gameObject);
            _sprites = loader.GetImages();
        }

        public Bitmap GetImage()
        {
            return _sprites[0];
        }
    }
}
