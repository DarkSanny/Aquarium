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

        public override void Collision(ObjectType objectType)
        {
	        if (objectType == GetCollisionType()) return;
	        //TODO: вызов события смерти
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
            else
            {
                //двигаться к цели
            }
        }
    }
}
