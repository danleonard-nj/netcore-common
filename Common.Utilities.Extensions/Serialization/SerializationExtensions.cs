/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Newtonsoft.Json;

namespace Common.Utilities.Extensions.Serialization
{
		public static class SerializationExtensions
		{
				public static string SerializeObject(this object obj)
				{
						return JsonConvert.SerializeObject(obj);
				}
		}
}
