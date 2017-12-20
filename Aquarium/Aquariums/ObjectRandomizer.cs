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

		private readonly Dictionary<ObjectType, int> _objectsCounter;
		private readonly IEnumerable<GameObject> _objects;

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
			_objectsCounter = new Dictionary<ObjectType, int>();
		}

		private ObjectRandomizer(IAquarium aquarium, Dictionary<ObjectType, int> objectsCounter, IEnumerable<GameObject> objects)
		{
			_aquarium = aquarium;
			_objectsCounter = objectsCounter;
			_objects = objects;
		}

		public ObjectRandomizer AddObject(ObjectType type, int countObjects)
		{
			if (_objectsCounter.ContainsKey(type))
				_objectsCounter[type] += Math.Max(0, countObjects);
			else 
				_objectsCounter[type] = Math.Max(0, countObjects);
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
			var defaultSize = new Size(80, 45);
			foreach (var objectsCounterKey in _objectsCounter.Keys)
			{
				var counter = _objectsCounter[objectsCounterKey];
				while (counter > 0)
				{
					var point = new Point(random.Next(aquariumSize.Width), random.Next(aquariumSize.Height));
					if (result.Any(o => o.Rectangle().IntersectsWith(GameObject.Rectangle(point, defaultSize))))
						continue;
					result.Add(ObjectBuilder[objectsCounterKey](_aquarium, point, Math.PI/180 * random.Next(360), defaultSize));
					counter--;
				}
			}
			return result;
		}
	}
}
