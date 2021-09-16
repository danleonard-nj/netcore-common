/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System.Collections.Generic;

namespace Common.Utilities.Extensions.Collections
{
		public static class CollectionExtensions
		{
				#region Dictionary Extensions

				public static T Get<T>(this Dictionary<string, object> dictionary, string key)
				{
						if (dictionary.ContainsKey(key))
						{
								return (T)dictionary[key];
						}

						return default;
				}

				public static object Get(this Dictionary<string, object> dictionary, string key)
				{
						if (dictionary.ContainsKey(key))
						{
								return dictionary[key];
						}

						return default;
				}

				public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> pairs, Dictionary<TKey, TValue> range)
				{
						foreach (var pair in range)
						{
								pairs.Add(pair.Key, pair.Value);
						}

						return pairs;
				}

				#endregion
		}
}
