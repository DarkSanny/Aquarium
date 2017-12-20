using System;
using System.Collections.Generic;
using System.Linq;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public class PiranhaBrain : Brain
	{
		private readonly Piranha _piranha;
		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;
		private static readonly HashSet<ObjectType> Food = new HashSet<ObjectType>() {ObjectType.BlueNeon};
		private static int _visorRadius = 300;

		public PiranhaBrain(Piranha piranha, IAquarium aquarium)
		{
			_piranha = piranha;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(Move);
		}

		private void Move()
		{
			_states.Push(Move);
			var food = _aquarium
				.GetFishes()
				.OfType<ICollise>()
				.Where(f => Food.Contains(f.GetCollisionType()))
				.ToList();
			if (food.Count == 0) return;
			OnTargetChanged(food.OfType<Fish>().MinItem(f => _piranha.DistanceTo(f)));
			_states.Push(MoveToTarget);
		}

		private void MoveToTarget()
		{
			if (_piranha.Target == null) return;
			if (!_aquarium.GetFishes().Contains(_piranha.Target)) return;
			if (_piranha.DistanceTo(_piranha.Target) > _visorRadius)
			{
				OnTargetChanged(null);
				return;
			}
			var targetLocation = _piranha.Target.GetLocation();
			var piranhaLocation = _piranha.GetLocation();
			var vector = new Vector(targetLocation.X - piranhaLocation.X, targetLocation.Y - piranhaLocation.Y);
			OnDirectionChanged(vector.Angle);
			_states.Push(MoveToTarget);
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
		}
	}
}