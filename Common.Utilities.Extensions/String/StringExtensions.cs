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

using Common.Utilities.Extensions.Reflection;

namespace Common.Utilities.Extensions.String
{
		public static class StringExtensions
		{
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
		}
}
