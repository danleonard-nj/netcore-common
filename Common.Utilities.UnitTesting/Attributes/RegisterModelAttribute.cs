/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System;
using System.Diagnostics.CodeAnalysis;

namespace Common.Utilities.UnitTesting.Attributes
{
		[ExcludeFromCodeCoverage]
		public class RegisterModelAttribute : Attribute
		{
				public string Key { get; set; }

				public RegisterModelAttribute(string key)
				{
						Key = key;
				}
		}
}
				
