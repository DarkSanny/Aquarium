using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium.UI
{
    public class ImageSource : IImage
    {
        private readonly List<Bitmap> _sprites;
	    private readonly List<Bitmap> _flippedSprites;
        private readonly int _animationCounter;
	    private readonly GameObject _gameObject;
	    private int _animationIndex;
        private int _counter;
	    private bool _isRight;

		private static readonly Dictionary<string, List<Bitmap>> _loadedImages = new Dictionary<string, List<Bitmap>>();

        public ImageSource(string objectName, int animationCounter, GameObject gameObject)
        {
	        if (_loadedImages.ContainsKey(objectName))
		        _sprites = _loadedImages[objectName];
	        else
	        {
				var loader = new ImageLoaderFromFile(objectName);
		        _sprites = loader.GetImages();
			}
	        _sprites = _sprites.Select(s => s.FlipHorisontal().FlipHorisontal()).ToList();
			_flippedSprites = new List<Bitmap>();
	        foreach (var sprite in _sprites)
	        {
		        _flippedSprites.Add(sprite.FlipHorisontal());
	        }
            _animationCounter = animationCounter;
	        _gameObject = gameObject;
	        _counter = animationCounter;
            _animationIndex = 0;
        }

        public Bitmap GetImage()
        {
	        if (IsShouldChangeDirection())
		        _isRight = !_isRight;
            _counter--;
	        if (_counter >= 0) return _isRight ? _flippedSprites[_animationIndex] : _sprites[_animationIndex]; 
			_counter = _animationCounter;
	        _animationIndex = (_animationIndex + 1) % _sprites.Count;
	        return _isRight ? _flippedSprites[_animationIndex] : _sprites[_animationIndex];
		}

	    private bool IsShouldChangeDirection()
	    {
		    if (!(_gameObject is Fish)) return false;
		    var fish = (Fish) _gameObject;
		    var angle = fish.Direction / (Math.PI / 180);
		    if (_isRight)
			    return angle > 100 && angle < 260;
		    return angle < 80 || angle > 280;
	    }
    }
}
