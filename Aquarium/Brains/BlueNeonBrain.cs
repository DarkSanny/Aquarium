using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquarium.Brains
{
	public class BlueNeonBrain : Brain
	{
		private readonly BlueNeon _neon;
		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;
		private static readonly HashSet<ObjectType> NaturalEnemies = new HashSet<ObjectType>() { ObjectType.Piranha };
		private GameObject _danger;

		public BlueNeonBrain(BlueNeon neon, IAquarium aquarium)
		{
			_neon = neon;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(_neon.IsLeader ? (Action) Move : MoveToTarget);
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
				.Where(f => NaturalEnemies.Contains(f.GetCollisionType()))
				.ToList();
			if (dangerous.Count == 0) return (false, null);
			var danger = dangerous.MinItem(f => f.DistanceTo(_neon));
			return danger != null ? (true, danger) : (false, null);
		}

		private void MoveToTarget()
		{
			var target = _neon.Target ?? _aquarium
											.GetFishes()
											.Where(f => f != _neon)
											.OfType<BlueNeon>()
											.FirstOrDefault(bn => bn.IsLeader);
			if (_neon.Target != target)	OnTargetChanged(target);
			if (target == null)
			{
				_neon.IsLeader = true;
				_states.Push(Move);
			}
			else
			{
				var targetLocation = target.GetLocation();
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
			if (_neon.DistanceTo(_danger) < 50)	_states.Push(MoveFromDangerous);
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
		}
	}
}
