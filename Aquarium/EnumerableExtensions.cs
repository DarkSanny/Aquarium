using System;
using System.Collections.Generic;

namespace Aquarium
{
	public static class EnumerableExtensions
	{

		public static T MinItem<T>(this IEnumerable<T> collection, Func<T, double> func)
		{
			var e = collection.GetEnumerator();
			var minItem = e.MoveNext() ? e.Current : default(T);
			var minValue = func(minItem);
			while (e.MoveNext())
			{
				var value = func(e.Current);
				if (!(value < minValue)) continue;
				minValue = value;
				minItem = e.Current;
			}
			return minItem;
		}

	}
}
