using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    public class ObjectDrawer : IObjectDrawer
    {
        private readonly Dictionary<Type, IImage> _imageFactory = new Dictionary<Type, IImage>();

        public void DrawObject(Graphics graphics, GameObject gameObject)
        {
            if (!_imageFactory.ContainsKey(gameObject.GetType()))
                _imageFactory.Add(gameObject.GetType(), new ImageSource(nameof(gameObject), 5));
            graphics.DrawImage(_imageFactory[gameObject.GetType()].GetImage(), gameObject.GetLocation());
        }
    }
}