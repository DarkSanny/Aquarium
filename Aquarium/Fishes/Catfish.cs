using System;
using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;

namespace Aquarium.Fishes
{
	public class Catfish : Fish
	{
		private readonly IAquarium _aquarium;
		private Point _location;
		private int sleep;
		private Random random = new Random();

		public Catfish(IAquarium aquarium, Point location, double direction,  Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Direction = direction;
			SetBrain(new CatfishBrain(this, aquarium));
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public override void Collision(ObjectType objectType, GameObject obj)
		{
			OnShouldDie();
		}

		public override ObjectType GetCollisionType()
		{
			return ObjectType.Catfish;
		}

		public override bool IsShouldCollise(ObjectType objectType)
		{
			return false;
		}

		public override void Move()
		{
			sleep--;
			if (sleep < 0) return;
			Brain.Think();
			_location = GetNextPoint(_aquarium);
			if (sleep < 50)
				sleep = random.Next(10) == 0 ? random.Next(50) : sleep;
		}
	}
}