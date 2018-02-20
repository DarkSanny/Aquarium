using System.Drawing;
using Aquarium.Aquariums;

namespace Aquarium.Fishes
{
	public class Flock	: Fish
	{
		private readonly IAquarium _aquarium;
		private Point _location;

		public Flock(IAquarium aquarium, Point location, double direction, Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Direction = direction;
			Speed = 6;
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public override void Move()
		{
			_location = GetNextPoint(_aquarium);
		}
	}
}
