using System;
using System.Collections.Generic;
using System.Drawing;
using Aquarium.Fishes;

namespace Aquarium.UI
{
    public class ObjectDrawer : IObjectDrawer
    {
        private readonly Dictionary<GameObject, IImage> _imageFactory = new Dictionary<GameObject, IImage>();
		private static readonly Dictionary<Type, string> ObjectNames = new Dictionary<Type, string>()
		{
			[typeof(BlueNeon)] = "BlueNeon",
			[typeof(Piranha)] = "Piranha" ,
			[typeof(Catfish)] = "CatFish",
			[typeof(Swordfish)]	= "SwordFish"
		};

        public void DrawObject(Graphics graphics, GameObject gameObject)
        {
	    if (!(gameObject is IDrawable)) return;
            if (!_imageFactory.ContainsKey(gameObject))
                _imageFactory.Add(gameObject, new ImageSource(ObjectNames[gameObject.GetType()], 2, gameObject));
            graphics.DrawImage(_imageFactory[gameObject].GetImage(), gameObject.Rectangle());
        }
    }
}