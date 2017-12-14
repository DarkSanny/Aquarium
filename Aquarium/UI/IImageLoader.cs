using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    interface IImageLoader
    {
        List<Bitmap> GetImages();
    }
}