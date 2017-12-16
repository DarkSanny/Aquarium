using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    public class ImageSource : IImage
    {
        private readonly List<Bitmap> _sprites;
        private int _animationCounter;
        private int _animationIndex;
        private int _counter;

        public ImageSource(string gameObject, int animationCounter)
        {
            var loader = new ImageLoaderFromFile(gameObject);
            _sprites = loader.GetImages();
            _animationCounter = animationCounter;
            _counter = animationCounter;
            _animationIndex = 0;
        }

        public Bitmap GetImage()
        {
            _counter--;
            if (_counter < 0)
            {
                _counter = _animationCounter;
                _animationIndex = (_animationIndex + 1) % _sprites.Count;
            }
            return _sprites[_animationIndex];
        }
    }
}
