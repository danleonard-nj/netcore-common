/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
