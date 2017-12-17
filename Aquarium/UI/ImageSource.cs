using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    public class ImageSource : IImage
    {
        private readonly List<Bitmap> _sprites;
	    private readonly List<Bitmap> _flippedSprites;
        private readonly int _animationCounter;
	    private int _animationIndex;
        private int _counter;
	    private bool _isRight;

		private static readonly Dictionary<string, List<Bitmap>> _loadedImages = new Dictionary<string, List<Bitmap>>();

        public ImageSource(string objectName, int animationCounter)
        {
	        if (_loadedImages.ContainsKey(objectName))
		        _sprites = _loadedImages[objectName];
	        else
	        {
				var loader = new ImageLoaderFromFile(objectName);
		        _sprites = loader.GetImages();
			}
			_flippedSprites = new List<Bitmap>();
	        foreach (var sprite in _sprites)
	        {
		        _flippedSprites.Add(sprite.FlipHorisontal());
	        }
            _animationCounter = animationCounter;
	        _counter = animationCounter;
            _animationIndex = 0;
        }

        public Bitmap GetImage()
        {
            _counter--;
	        if (_counter >= 0) return _isRight ? _flippedSprites[_animationIndex] : _sprites[_animationIndex];
	        _isRight = !_isRight;
			_counter = _animationCounter;
	        _animationIndex = (_animationIndex + 1) % _sprites.Count;
	        return _isRight ? _flippedSprites[_animationIndex] : _sprites[_animationIndex];
		}
    }
}
