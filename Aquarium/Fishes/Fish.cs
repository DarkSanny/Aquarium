using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Brains;

namespace Aquarium.Fishes
{
	public enum ObjectType
	{
		BlueNeon,
		Piranha
	}

	public abstract class Fish : GameObject, ICollise
	{
		protected Brain Brain;
		private readonly Size _size;
		private double _direction;

		public double Direction
		{
			get => _direction;
			protected set => _direction = value % (2 * Math.PI);
		}
		public double Speed { get; protected set; }
		public int Force { get; protected set; }
		public Fish Target { get; protected set; }

		protected Fish(Size size)
		{
			_size = size;
		}

		protected void SetBrain(Brain brain)
		{
			Brain = brain;
			Brain.DirectionChanged += (direction) => Direction = direction;
			Brain.TargetChanged += (target) => Target = target;
		}

		public override Size GetSize()
		{
			return _size;
		}

		public abstract void Collision(ObjectType objectType, GameObject obj);

		public abstract ObjectType GetCollisionType();

		public abstract void Move();

		public abstract bool IsShouldCollise(ObjectType objectType);

		protected Point GetNextPoint(IAquarium aquarium)
		{
			while (true)
			{
				var nextPoint = GetCartesianPoint();
				if (IsOutOfBorder(nextPoint, aquarium))
					Direction = Bounce(Direction, nextPoint, aquarium);
				else
				{
					var intersections = aquarium.GetObjects()
						.Where(o => o != this && o.Rectangle().IntersectsWith(Rectangle(nextPoint, GetSize())));
					var gameObjects = intersections as IList<GameObject> ?? intersections.ToList();
					if (!gameObjects.Any()) return nextPoint;
					var collisions = gameObjects.OfType<ICollise>();
					if (collisions.All(c => IsShouldCollise(c.GetCollisionType()))) return nextPoint;
					return GetLocation();
				}
			}
		}

		private static bool IsOutOfBorder(Point point, IAquarium aquarium)
		{
			var size = aquarium.GetSize();
			return point.X < 0 || point.X > size.Width || point.Y < 0 ||
			       point.Y > size.Height;
		}

		private static double Bounce(double directionRadians, Point point, IAquarium aquarium)
		{
			var size = aquarium.GetSize();
			var wallInclinationRadians = 0.0;
			if (point.X < 0 || point.X > size.Width) wallInclinationRadians = Math.PI / 2;
			if (point.Y < 0 || point.Y > size.Height) wallInclinationRadians = 0;
			return 2 * wallInclinationRadians - directionRadians;
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