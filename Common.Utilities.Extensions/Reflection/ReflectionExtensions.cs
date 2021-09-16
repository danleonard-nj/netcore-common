/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities.Extensions.Reflection
{
		public static class ReflectionExtensions
		{
				public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this object values)
				{
						var props = values.GetType().GetProperties();

						var pairs = props.Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(values).ToString()));

						return pairs;
				}
		}
}
