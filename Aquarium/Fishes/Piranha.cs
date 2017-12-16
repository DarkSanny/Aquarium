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
			Speed = 3;
			Force = 1;
			SetBrain(new PiranhaBrain(this, _aquarium));
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public override void Collision(ObjectType objectType, GameObject obj)
		{
			if (objectType == GetCollisionType()) return;
			if (objectType != ObjectType.BlueNeon) 
				OnShouldDie();
		}

		public override ObjectType GetCollisionType()
		{
			return ObjectType.Piranha;
		}

		public override bool IsShouldCollise(ObjectType objectType)
		{
			return objectType == ObjectType.BlueNeon;
		}

		public override void Move()
		{
			Brain.Think();
			_location = GetNextPoint(_aquarium);
		}
	}
}
