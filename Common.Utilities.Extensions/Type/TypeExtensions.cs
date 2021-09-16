/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System;

namespace Common.Utilities.Extensions.Type
{
		public static class TypeExtensions
		{
				public static T AsType<T>(this object obj)
				{
						var newType = Convert.ChangeType(obj, typeof(T));

						return (T)newType;
				}
		}
}
