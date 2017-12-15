using System.Collections.Generic;
using System.Drawing;

namespace Aquarium.UI
{
    public interface IImageLoader
    {
        List<Bitmap> GetImages();
    }
}