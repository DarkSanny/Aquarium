using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;

namespace Aquarium.Fishes
{
	public class Piranha : Fish
	{
		private readonly IAquarium _aquarium;
		private Point _location;

		public Piranha(IAquarium aquarium, Point location, double direction, Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Direction = direction;
			Speed = 5;
			Force = 1;
			SetBrain(new PiranhaBrain(this, _aquarium));
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public override void Collision(IObject obj)
		{
			if (!(obj is ICollise)) return;
			var colliser = (ICollise)obj;
			if (colliser.GetCollisionType() == GetCollisionType()) return;
			if (colliser.GetCollisionType() != ObjectType.BlueNeon)
				OnShouldDie();
			else Target = null;
		}

		public override ObjectType GetCollisionType()
		{
			return ObjectType.Piranha;
		}

		public override bool IsShouldCollise(IObject obj)
		{
			if (!(obj is ICollise)) return true;
			if (obj is Fish fish)
			{
				if (fish.Force < Force) return true;
			}
			var colliser = (ICollise)obj;
			return colliser.GetCollisionType() == ObjectType.BlueNeon;
		}

		public override void Move()
		{
			Brain.Think();
			_location = GetNextPoint(_aquarium);
		}
	}
}
