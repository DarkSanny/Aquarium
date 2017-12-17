using System.Drawing;
using System.Drawing.Imaging;

namespace Aquarium.UI
{
	public static class BitmapExtensions
	{
		public static Bitmap ImageClone(this Bitmap bitmap)
		{
			return bitmap.ImageClone(bitmap.PixelFormat);
		}

		public static Bitmap ImageClone(this Bitmap bitmap, PixelFormat format)
		{
			var clone = new Bitmap(bitmap.Width, bitmap.Height, format);
			using (var graphics = Graphics.FromImage(clone))
				graphics.DrawImage(bitmap, 0, 0);
			return clone;
		}

		public static Bitmap FlipHorisontal(this Bitmap bitmap)
		{
			var clone = bitmap.ImageClone();
			clone.RotateFlip(RotateFlipType.RotateNoneFlipX);
			return clone;
		}

		public static Bitmap FlipVertical(this Bitmap bitmap)
		{
			var clone = bitmap.ImageClone();
			clone.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return clone;
		}
	}
}