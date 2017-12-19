using System;
using System.Collections.Generic;
using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public class SwordfishBrain	: Brain
	{
		private readonly Swordfish _swordfish;
		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;
		private Point _lastPosition;
		private readonly Random _random = new Random();

		public SwordfishBrain(Swordfish swordfish, IAquarium aquarium)
		{
			_swordfish = swordfish;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(Move);
			_lastPosition = new Point(0, 0);
		}

		public void Move()
		{
			  _states.Push(Move);
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
			if (_lastPosition == _swordfish.GetLocation())
				OnDirectionChanged(_random.Next(360) * Math.PI / 180);
			_lastPosition = _swordfish.GetLocation();
		}
	}
}
