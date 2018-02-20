using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium.Aquariums
{
	public class ObjectRandomizer : IObjectProvider
	{
		private readonly IAquarium _aquarium;

		private readonly Dictionary<Tuple<ObjectType, Size>, int> _objectsCounter;
		private readonly IEnumerable<GameObject> _objects;
		private readonly Size _defaultSize = new Size(80, 45);

		private static readonly Dictionary<ObjectType, Func<IAquarium, Point,double, Size, GameObject>> ObjectBuilder 
			= new Dictionary<ObjectType, Func<IAquarium, Point, double, Size, GameObject>>()
			{
				[ObjectType.BlueNeon] = (a, p, d, s) => new BlueNeon(a, p, d, s),
				[ObjectType.Piranha] = (a, p, d, s) => new Piranha(a, p, d, s),
				[ObjectType.Catfish] = (a, p, d, s) => new Catfish(a, p, d, s),
				[ObjectType.Swordfish] = (a, p, d, s) => new Swordfish(a, p, d, s)
			};

		public ObjectRandomizer(IAquarium aquarium)
		{
			_aquarium = aquarium;
			_objects = new List<GameObject>();
			_objectsCounter = new Dictionary<Tuple<ObjectType, Size>, int>();
		}

		private ObjectRandomizer(IAquarium aquarium, Dictionary<Tuple<ObjectType, Size>, int> objectsCounter, IEnumerable<GameObject> objects)
		{
			_aquarium = aquarium;
			_objectsCounter = objectsCounter;
			_objects = objects;
		}

		public ObjectRandomizer AddObject(ObjectType type, int countObjects)
		{
			return AddObject(type, countObjects, _defaultSize);
		}

		public ObjectRandomizer AddObject(ObjectType type, int countObjects, Size size)
		{
			var tuple = Tuple.Create(type, size);
			if (_objectsCounter.ContainsKey(tuple))
				_objectsCounter[tuple] += Math.Max(0, countObjects);
			else
				_objectsCounter[tuple] = Math.Max(0, countObjects);
			return new ObjectRandomizer(_aquarium, _objectsCounter, _objects);
		}

		public ObjectRandomizer WithObjects(IEnumerable<GameObject> objects)
		{
			   return new ObjectRandomizer(_aquarium, _objectsCounter, _objects.Concat(objects));
		}

		public List<GameObject> GetObjects()
		{
			var result = _objects.ToList();
			var random = new Random();
			var aquariumSize = _aquarium.GetSize();
			foreach (var objectsCounterKey in _objectsCounter.Keys)
			{
				var counter = _objectsCounter[objectsCounterKey];
				while (counter > 0)
				{
					var point = new Point(random.Next(aquariumSize.Width), random.Next(aquariumSize.Height));
					if (result.Any(o => o.Rectangle().IntersectsWith(GameObject.Rectangle(point, objectsCounterKey.Item2))))
						continue;
					result.Add(ObjectBuilder[objectsCounterKey.Item1](_aquarium, point, Math.PI/180 * random.Next(360), objectsCounterKey.Item2));
					counter--;
				}
			}
			return result;
		}
	}
}
