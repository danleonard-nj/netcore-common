/* Copyright (C) 2021 Dan Leonard
 * 
 * This is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */


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
