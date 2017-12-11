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
	}
}
