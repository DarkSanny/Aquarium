using System;
using System.Drawing;

namespace Aquarium
{
	public enum ObjectType
	{
		BlueNeon
	}

	public abstract class Fish : IObject, ICollise
	{
		public double Direction { get; protected set; }
		public double Speed { get; protected set; }
		public int Force { get; protected set; }
		public Fish Target { get; protected set; }

		public abstract void Collision(ObjectType objectType);

		public abstract ObjectType GetCollisionType();

		public abstract void Move();

		public abstract bool IsShouldCollise(ObjectType objectType);
		public abstract Point GetLocation();
		public abstract Size GetSize();

		protected Point GetNextPoint(IAquarium aquarium)
		{
			while (true)
			{
				var nextPoint = GetCartesianPoint();
				if (nextPoint.X >= 0 && nextPoint.X <= aquarium.GetSize().Width && nextPoint.Y >= 0 &&
				    nextPoint.Y <= aquarium.GetSize().Height) return nextPoint;
				Direction = (Direction + Math.PI) % 2 * Math.PI;
			}
		}

		private Point GetCartesianPoint()
		{
			return new Point(GetLocation().X + (int) (Speed * Math.Cos(Direction)),
				GetLocation().Y + (int) (Speed * Math.Sin(Direction)));
		}
	}
}