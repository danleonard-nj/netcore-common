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


using System;

namespace Common.Utilities.DependencyInjection.Exports.Types
{
		public interface ISettingsExport
		{
				Type Type { get; }
				string TypeName { get; }
		}

		public class SettingsExport : ISettingsExport
		{
				public Type Type { get => _type; }
				public string TypeName { get => _type.Name; }

				public SettingsExport(Type type)
				{
						_type = type;
				}

				private readonly Type _type;
		}

		public class SettingsExport<T> : ISettingsExport
		{
				public Type Type { get => typeof(T); }
				public string TypeName { get => typeof(T).Name; }
		}
}
