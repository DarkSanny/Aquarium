using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    class ImageSource : IImage
    {
        private List<Bitmap> sprites;

        public ImageSource(string gameObject)
        {
            ImageLoaderFromFile loader = new ImageLoaderFromFile(gameObject);
            sprites = loader.GetImages();
        }

        public Bitmap GetImage()
        {
            return sprites[0];
        }
    }
}
