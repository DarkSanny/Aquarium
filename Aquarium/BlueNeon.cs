using System.Drawing;
using System.Linq;
using Aquarium.Brains;

namespace Aquarium
{
    public class BlueNeon : Fish
    {
        private readonly IAquarium _aquarium;
        private Point _location;
        public bool IsLeader { get; private set; }

        public BlueNeon(IAquarium aquarium, Point location, double direction, Size size) : base(new BlueNeonBrain(), size)
        {
            _aquarium = aquarium;
            _location = location;
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

        public override bool IsShouldCollise(ObjectType objectType)
        {
			// todo: false если рыбу нельзя съесть
            return objectType != GetCollisionType() || IsLeader;
        }

        public override void Move()
        {
			/*/
			 * todo: в этом метсте должен думать мозг, события установят напраыление и после нужно просто попробывать сдвинуться вперед
			 * все что снизу это простая реализация интелекта для рыбы, нужен автомат
			 * /*/
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
	            var vector = new Vector(targetLocation.X - _location.X, targetLocation.Y - _location.Y);
	            Direction = vector.Angle;
	            _location = GetNextPoint(_aquarium);
            }
        }
    }
}
