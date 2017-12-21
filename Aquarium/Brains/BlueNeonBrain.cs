using System;
using System.Collections.Generic;
using System.Linq;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public class BlueNeonBrain : Brain
	{
		private readonly BlueNeon _neon;
		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;
		private GameObject _danger;
		private const int DangerRadius = 300;

		public BlueNeonBrain(BlueNeon neon, IAquarium aquarium)
		{
			_neon = neon;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(MoveToTarget);
		}

		private void Move()
		{
			_states.Push(Move);
			var dangerous = IsDangerous();
			if (_danger == null && !dangerous.IsDangerous) return;
			_danger = dangerous.danger;
			_states.Push(MoveFromDangerous);
		}

		private (bool IsDangerous, GameObject danger) IsDangerous()
		{
			var dangerous =  _aquarium
				.GetFishes()
				.OfType<ICollise>()
				.Where(f => f.IsShouldCollise(_neon))
				.ToList();
			if (dangerous.Count == 0) return (false, null);
			var danger = (Fish)dangerous.MinItem(f => ((Fish)f).DistanceTo(_neon));
			return danger == null ? (false, null) : _neon.DistanceTo(danger) < DangerRadius ? (true, danger) : (false, null);
		}

		private void MoveToTarget()
		{
			var flocks = _aquarium
				.GetFishes()
				.OfType<Flock>()
				.ToList();
			if (flocks.Count == 0)
			{
				OnTargetChanged(null);
				_states.Push(Move);
			}
			else
			{
				var shortesFlock = flocks.MinItem(f => f.DistanceTo(_neon));
				OnTargetChanged(shortesFlock);
				var targetLocation = shortesFlock.GetLocation();
				var neonLocation = _neon.GetLocation();
				var vector = new Vector(targetLocation.X - neonLocation.X, targetLocation.Y - neonLocation.Y);
				OnDirectionChanged(vector.Angle);
				_states.Push(MoveToTarget);
			}
			var dangerous = IsDangerous();
			if (_danger == null && !dangerous.IsDangerous) return;
			_danger = dangerous.danger;
			_states.Push(MoveFromDangerous);
		}

		private void MoveFromDangerous()
		{
			var targetLocation = _danger.GetLocation();
			var neonLocation = _neon.GetLocation();
			var vector = new Vector(targetLocation.X - neonLocation.X, targetLocation.Y - neonLocation.Y);
			OnDirectionChanged(vector.Angle + Math.PI);
			if (_neon.DistanceTo(_danger) < DangerRadius)
				_states.Push(MoveFromDangerous);
			else
				_danger = null;
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
		}
	}
}
