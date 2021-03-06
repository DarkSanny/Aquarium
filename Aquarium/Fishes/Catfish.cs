﻿using System;
using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Brains;
using Aquarium.UI;

namespace Aquarium.Fishes
{
	public class Catfish : Fish, IDrawable, ICollise
	{
		private readonly IAquarium _aquarium;
		private Point _location;
		private int _sleep;
		private readonly Random _random = new Random();

		public Catfish(IAquarium aquarium, Point location, double direction, Size size) : base(size)
		{
			_aquarium = aquarium;
			_location = location;
			Direction = direction;
			Speed = 3;
			Force = 2;
			_sleep = 1000;
			SetBrain(new CatfishBrain(this, aquarium));
		}

		public override Point GetLocation()
		{
			return _location;
		}

		public void Collision(IObject obj)
		{
			if (!(obj is ICollise)) return;
			OnShouldDie();
		}

		public ObjectType GetCollisionType()
		{
			return ObjectType.Catfish;
		}

		public bool IsShouldCollise(IObject obj)
		{
			return false;
		}

		public override void Move()
		{
			_sleep--;
			if (_sleep < -50)
				_sleep = _random.Next(2) == 0 ? _random.Next(1000) : _sleep;
			if (_sleep < 0) return;
			Brain.Think();
			_location = GetNextPoint(_aquarium);
		}
	}
}