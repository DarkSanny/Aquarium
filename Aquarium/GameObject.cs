using System;
using System.Drawing;

namespace Aquarium
{
	public abstract class GameObject : IObject
	{
		public abstract Point GetLocation();

		public abstract Size GetSize();

		public Rectangle Rectangle()
		{
			var location = GetLocation();
			var size = GetSize();
			return new Rectangle(location.X - size.Width/2, location.Y - size.Height/2, size.Width, size.Height);
		}

		public static Rectangle Rectangle(Point location, Size size)
		{
			return new Rectangle(location.X - size.Width / 2, location.Y - size.Height / 2, size.Width, size.Height);
		}

		public double DistanceTo(GameObject gameObject)
		{
			var point1 = GetLocation();
			var point2 = gameObject.GetLocation();
			var dx = point1.X - point2.X;
			var dy = point1.Y - point2.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}
	}
}
