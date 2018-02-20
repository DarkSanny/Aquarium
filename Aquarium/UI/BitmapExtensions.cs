using System.Drawing;

namespace Aquarium.UI
{
	public static class BitmapExtensions
	{
		public static Bitmap FlipHorisontal(this Bitmap bitmap)
		{
			var clone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
			clone.RotateFlip(RotateFlipType.RotateNoneFlipX);
			return clone;
		}

		public static Bitmap FlipVertical(this Bitmap bitmap)
		{
			var clone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
			clone.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return clone;
		}
	}
}