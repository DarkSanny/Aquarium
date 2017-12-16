﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium.Aquariums
{
	public class SimpleAquarium : IAquarium
	{
		private readonly Size _size;
		private readonly List<GameObject> _objects;
		private readonly Stack<GameObject> _deadFishes;

		public SimpleAquarium(Size size, int fishCount)
		{
			_size = size;
			_objects = new ObjectRandomizer(this)
				.AddObject(ObjectType.BlueNeon, fishCount)
				.GetObjects();
			foreach (var fish in GetFishes())
				fish.ShouldDie += () => _deadFishes.Push(fish);
			_deadFishes = new Stack<GameObject>();
			_objects.Add(new Piranha(this, new Point(50, 50), Math.PI/3, new Size(70, 40)));
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
