using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    class Drawing : IObjectDrawer
    {
        Dictionary<Type, IImage> ImageFactory = new Dictionary<Type, IImage>();

        public void DrawObject(Graphics graphics, GameObject gameObject)
        {
            if (!ImageFactory.ContainsKey(gameObject.GetType()))
                ImageFactory.Add(gameObject.GetType(), new ImageSource(gameObject.ToString()));
            graphics.DrawImage(ImageFactory[gameObject.GetType()].GetImages()[0], gameObject.GetLocation());
        }
    }
}
