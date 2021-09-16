/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
