using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium.Aquariums
{
	public class SimpleAquarium : IAquarium
	{
		private readonly Size _size;
		private List<GameObject> _objects;
		private readonly Stack<GameObject> _deadFishes;

		public SimpleAquarium(Size size)
		{
			_size = size;
			_deadFishes = new Stack<GameObject>();
		}

		public void Start(IObjectProvider provider)
		{
			_objects = provider.GetObjects();
			foreach (var fish in GetFishes())
				fish.ShouldDie += () => _deadFishes.Push(fish);
			foreach (var blueNeon in GetFishes().OfType<BlueNeon>())
				blueNeon.ShouldDie += () => GetFishes().OfType<BlueNeon>().Where(f => f == blueNeon).ToList().ForEach(f => f.Target = null);
		}

        private void AddGameObject(GameObject gameObject)
        {
            _objects.Add(gameObject);
	        if (gameObject is Fish fish)
		        fish.ShouldDie += () => _deadFishes.Push(fish);
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
			var fishes = GetFishes().ToList();
			foreach (var fish in fishes)
			{
				var f1 = fishes.Where(f => f != fish).Where(f => f.Rectangle().IntersectsWith(fish.Rectangle()));
				f1.ToList().ForEach(f => f.Collision(fish));
			}
		}
	}
}
