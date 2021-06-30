/* Copyright (C) 2012, 2013 Dan Leonard
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
