using System;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public abstract class Brain
	{
		public abstract void Think();

		public event Action<double> DirectionChanged;
		public event Action<GameObject> TargetChanged;

		protected virtual void OnDirectionChanged(double direction)
		{
			DirectionChanged?.Invoke(direction);
		}

		protected virtual void OnTargetChanged(GameObject target)
		{
			TargetChanged?.Invoke(target);
		}

	}
}
