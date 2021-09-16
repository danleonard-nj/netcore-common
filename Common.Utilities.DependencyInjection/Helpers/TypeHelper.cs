/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.DependencyInjection.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Utilities.DependencyInjection.Helpers
{
		public static class TypeHelper
		{
				public static Type GetImplementationType(Type serviceType)
				{
						var implementationTypes = Assembly.GetEntryAssembly()
								.DefinedTypes
								.Where(type => serviceType.IsAssignableFrom(type.AsType()))
								.Where(type => !type.IsInterface)
								.ToList();

						if (implementationTypes == null)
						{
								throw new DependencyInjectionException("No valid implementation found for service type", serviceType);
						}

						if (implementationTypes.Count > 1)
						{
								throw new DependencyInjectionException("Service type has more than one implementation", serviceType);
						}

						var implementationType = implementationTypes.FirstOrDefault();

						return implementationType;
								
				}
		}
}
