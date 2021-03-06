﻿using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;
using Aquarium.UI;

namespace Aquarium.Fishes
{
	public class Swordfish : Fish, IDrawable, ICollise
	{
		private readonly IAquarium _aquarium;
		private Point _location;

		public Swordfish(IAquarium aquarium, Point location, double direction, Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Speed = 6;
			Force = 1;
			Direction = direction;
			SetBrain(new SwordfishBrain(this, aquarium));
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public void Collision(IObject obj)
		{
			if (!(obj is ICollise)) return;
			var objSize = obj.GetSize();
			var mySize = GetSize();
			var collise = (ICollise) obj;
			if (collise.GetCollisionType() == GetCollisionType() &&
			    objSize.Width * objSize.Height >= mySize.Width * mySize.Height / 2)
				OnShouldDie();
		}

		public ObjectType GetCollisionType()
		{
			return ObjectType.Swordfish;
		}

		public bool IsShouldCollise(IObject obj)
		{
			if (!(obj is ICollise)) return true;
			var collise = (ICollise) obj;
			var objSize = obj.GetSize();
			var mySize = GetSize();
			return collise.GetCollisionType() != GetCollisionType() &&
			       objSize.Width * objSize.Height < mySize.Width * mySize.Height / 2;
		}

		public override void Move()
		{
			Brain.Think();
			_location = GetNextPoint(_aquarium);
		}
	}
}