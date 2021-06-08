/* Copyright (C) 2021 Dan Leonard
 * 
 * This  is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities.Extensions
{
		public static class CommonExtensions
		{
				#region String Extensions

				public static string Format(this string input, object values)
				{
						var pairs = values.ToKeyValuePairs();

						foreach (var pair in pairs)
						{
								var key = "{" + pair.Key + "}";
								input = input.Replace(key, pair.Value);
						}

						return input;
				}

				#endregion

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

				#region Reflection Extensions

				public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this object values)
				{
						var props = values.GetType().GetProperties();

						var pairs = props.Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(values).ToString()));

						return pairs;
				}

				#endregion

				#region Type Extensions

				public static T AsType<T>(this object obj)
				{
						var newType = Convert.ChangeType(obj, typeof(T));

						return (T)newType;
				}

				#endregion

				#region Serialization Extensions

				public static string SerializeObject(this object obj)
				{
						return JsonConvert.SerializeObject(obj);
				}

				#endregion
		}
}