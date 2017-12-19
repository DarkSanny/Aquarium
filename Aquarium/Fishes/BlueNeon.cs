using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;

namespace Aquarium.Fishes
{
    public class BlueNeon : Fish
    {
        private readonly IAquarium _aquarium;
        private Point _location;
        public bool IsLeader { get; set; }

        public BlueNeon(IAquarium aquarium, Point location, double direction, Size size) : base(size)
        {
            _aquarium = aquarium;
            _location = location;
            Speed = 7;
            Force = 0;
            Direction = direction;
			SetBrain(new BlueNeonBrain(this, aquarium));
        }

        public override void Collision(IObject obj)
        {
	        if (!(obj is ICollise)) return;
	        var colliser = (ICollise) obj;
	        if (colliser.GetCollisionType() == GetCollisionType()) return;
	        OnShouldDie();
        }

        public override ObjectType GetCollisionType()
        {
            return ObjectType.BlueNeon;
        }

        public override Point GetLocation()
        {
            return _location;
        }

        public override bool IsShouldCollise(IObject obj)
        {
	        if (!(obj is ICollise)) return true;
	        var collise = (ICollise) obj;
	        return collise.GetCollisionType() == GetCollisionType() && IsLeader;
        }

        public override void Move()
        {
			Brain.Think();
	        _location = GetNextPoint(_aquarium);
        }
    }
}
