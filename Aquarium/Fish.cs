using System;
using System.Drawing;
using System.Linq;

namespace Aquarium
{
	public enum ObjectType
	{
		BlueNeon
	}

	public abstract class Fish : GameObject, ICollise
	{
		public double Direction { get; protected set; }
		public double Speed { get; protected set; }
		public int Force { get; protected set; }
		public Fish Target { get; protected set; }

		public abstract void Collision(ObjectType objectType, GameObject obj);

		public abstract ObjectType GetCollisionType();

		public abstract void Move();

		public abstract bool IsShouldCollise(ObjectType objectType);

		protected Point GetNextPoint(IAquarium aquarium)
		{
			while (true)
			{
				var nextPoint = GetCartesianPoint();
				if (nextPoint.X < 0 || nextPoint.X > aquarium.GetSize().Width || nextPoint.Y < 0 ||
				    nextPoint.Y > aquarium.GetSize().Height)
					Direction = (Direction + Math.PI) % 2 * Math.PI;
				else if (aquarium.GetObjects().Where(o => o != this).Where(o => o.Rectangle().IntersectsWith(Rectangle())).OfType<ICollise>().Count(c => c.IsShouldCollise(GetCollisionType())) > 0)
					return GetLocation();
				else return nextPoint;
			}
		}

		private Point GetCartesianPoint()
		{
			return new Point(GetLocation().X + (int) (Speed * Math.Cos(Direction)),
				GetLocation().Y + (int) (Speed * Math.Sin(Direction)));
		}

		public event Action ShouldDie;

		protected virtual void OnShouldDie()
		{
			ShouldDie?.Invoke();
		}
	}
}