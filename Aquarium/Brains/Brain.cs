using System;

namespace Aquarium.Brains
{
	public abstract class Brain
	{
		public abstract void Think();

		public event Action<double> DirectionChanged;
		public event Action<Fish> TargetChanged;

		protected virtual void OnDirectionChanged(double direction)
		{
			DirectionChanged?.Invoke(direction);
		}

		protected virtual void OnTargetChanged(Fish target)
		{
			TargetChanged?.Invoke(target);
		}

	}
}
