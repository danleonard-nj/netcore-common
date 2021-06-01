/* Copyright (C) 2021 Dan Leonard
 * 
 * This  is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */

using Common.Utilities.DependencyInjection.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Utilities.DependencyInjection.Helpers
{
		public static class TypeHelper
		{
				public static object GetInstance(IConfiguration configuration, Type instanceType)
				{
						var instance = Activator.CreateInstance(instanceType);

						configuration.GetSection(instanceType.Name).Bind(instance);

						return instance;
				}

				public static T GetInstance<T>(IConfiguration configuration)
				{
						var instance = Activator.CreateInstance(typeof(T));

						configuration.GetSection(typeof(T).Name).Bind(instance);

						return (T)instance;
				}

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
