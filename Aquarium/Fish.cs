using System;
using System.Collections.Generic;
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
				else
				{
					var intersections = aquarium.GetObjects().Where(o => o != this && o.Rectangle().IntersectsWith(Rectangle(nextPoint, GetSize())));
					var gameObjects = intersections as IList<GameObject> ?? intersections.ToList();
					if (!gameObjects.Any()) return nextPoint;
					var collisions = gameObjects.OfType<ICollise>();
					if (collisions.All(c => IsShouldCollise(c.GetCollisionType()))) return nextPoint;
					return GetLocation();

				}
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