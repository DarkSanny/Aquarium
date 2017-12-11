using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Aquarium
{
	public class Aquarium : IAquarium
	{
		private readonly Size _size;
		private readonly IEnumerable<GameObject> _objects;

		public Aquarium(Size size, int fishCount)
		{
			_size = size;
			_objects = GenerateFishes(fishCount);
			
		}

		private IEnumerable<GameObject> GenerateFishes(int fishCount)
		{
			var result = new List<GameObject>();
			for (var i = 0; i < fishCount/2; i++)
				result.Add(new BlueNeon(this, new Point(20 + i*20, 20), 0));
			for (var i = 0; i < fishCount / 2; i++)
				result.Add(new BlueNeon(this, new Point(20 + i * 30, 40), 0));
			return result;
		}

		public Size GetSize()
		{
			return _size;
		}

		public IEnumerable<GameObject> GetObjects()
		{
			return _objects;
		}

		public IEnumerable<Fish> GetFishes()
		{
			return _objects.OfType<Fish>();
		}
	}
}
