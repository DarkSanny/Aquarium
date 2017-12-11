using System.Drawing;
using System.Linq;

namespace Aquarium
{
    public class BlueNeon : Fish
    {
        private readonly IAquarium _aquarium;
        private Point _location;
        private readonly Size _size;
        public bool IsLeader { get; private set; }

        public BlueNeon(IAquarium aquarium, Point location, double direction)
        {
            _aquarium = aquarium;
            _location = location;
            _size = new Size(20, 10);
            Speed = 5;
            Force = 0;
            Direction = direction;
        }

        public override void Collision(ObjectType objectType, GameObject obj)
        {
	        if (objectType == GetCollisionType()) return;
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

        public override Size GetSize()
        {
            return _size;
        }

        public override bool IsShouldCollise(ObjectType objectType)
        {
			// todo: false если рыбу нельзя съесть
	        //if (objectType == GetCollisionType() && IsLeader) return true;
            return objectType != GetCollisionType() || IsLeader;
        }

        public override void Move()
        {
            if (!IsLeader && Target == null) 
                Target = _aquarium
                    .GetFishes()
                    .OfType<BlueNeon>()
                    .FirstOrDefault(bn => bn.IsLeader);
            if (Target == null) IsLeader = true;
            if (IsLeader)
            {
                _location = GetNextPoint(_aquarium);
            }
            else if (Target != null)
            {
	            var targetLocation = Target.GetLocation();
				var vector = new Vector(targetLocation.X-_location.X, targetLocation.Y - _location.Y);
	            Direction = vector.Angle;
	            _location = GetNextPoint(_aquarium);
            }
        }
    }
}
