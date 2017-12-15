using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium
{
	public class Aquarium : IAquarium
	{
		private readonly Size _size;
		private readonly List<GameObject> _objects;
		private readonly Stack<GameObject> _deadFishes;

		public Aquarium(Size size, int fishCount)
		{
			_size = size;
			_objects = GenerateFishes(fishCount);
			_deadFishes = new Stack<GameObject>();
		}

		private List<GameObject> GenerateFishes(int fishCount)
		{
			var result = new List<GameObject>();
			for (var i = 0; i < fishCount / 2; i++)
			{
				var neon = new BlueNeon(this, new Point(20 + i * 20, 20), Math.PI/3, new Size(20, 10));
				neon.ShouldDie += () => _deadFishes.Push(neon);
				result.Add(neon);
			}
			for (var i = 0; i < fishCount / 2; i++)
			{
				var neon = new BlueNeon(this, new Point(20 + i * 20, 40), Math.PI/3, new Size(20, 10));
				neon.ShouldDie += () => _deadFishes.Push(neon);
				result.Add(neon);
			}
			return result;
		}

        public void AddGameObject(GameObject gameObject)
        {
            _objects.Add(gameObject);
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

		public void Update()
		{
			foreach (var gameObject in _deadFishes)
			{
				_objects.Remove(gameObject);
			}
			GetFishes().ToList().ForEach(i => i.Move());
		}
	}
}
