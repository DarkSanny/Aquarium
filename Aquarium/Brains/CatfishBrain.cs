using System;
using System.Collections.Generic;
using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public class CatfishBrain : Brain
	{
		private readonly Catfish _catfish;

		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;
		private Point _lastPosition;
		private readonly Random _random = new Random();
		private Point _targetLocation;

		public CatfishBrain(Catfish catfish, IAquarium aquarium)
		{
			_catfish = catfish;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(Move);
			_lastPosition = new Point(0, 0);
			_targetLocation = new Point(_aquarium.GetSize().Width, _aquarium.GetSize().Width);
		}

		private void Move()
		{
			_states.Push(Move);
			if (_catfish.GetLocation().Y < _aquarium.GetSize().Height - _aquarium.GetSize().Height / 3)
				_states.Push(MoveDown);
		}

		private void MoveDown()
		{
			if (_catfish.GetLocation().Y < _aquarium.GetSize().Height - _aquarium.GetSize().Height / 3)
				_states.Push(MoveDown);
			var catfishLocation = _catfish.GetLocation();
			var vector = new Vector(_targetLocation.X - catfishLocation.X, _targetLocation.Y - catfishLocation.Y);
			OnDirectionChanged(vector.Angle);
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
			if (_lastPosition == _catfish.GetLocation())
				_targetLocation = new Point(_random.Next(_aquarium.GetSize().Width), _aquarium.GetSize().Height);
			_lastPosition = _catfish.GetLocation();
		}
	}
}