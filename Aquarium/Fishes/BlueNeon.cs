using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;

namespace Aquarium.Fishes
{
	public class BlueNeon : Fish, IDrawable, ICollise
	{
		private readonly IAquarium _aquarium;
		private Point _location;

		public BlueNeon(IAquarium aquarium, Point location, double direction, Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Speed = 6;
			Force = 0;
			Direction = direction;
			SetBrain(new BlueNeonBrain(this, aquarium));
		}

		public void Collision(IObject obj)
		{
			if (!(obj is ICollise)) return;
			OnShouldDie();
		}

		public ObjectType GetCollisionType()
		{
			return ObjectType.BlueNeon;
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public bool IsShouldCollise(IObject obj)
		{
			return false;
		}

		public override void Move()
		{
			Brain.Think();
			_location = GetNextPoint(_aquarium);
		}
	}
}