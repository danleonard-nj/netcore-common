/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.UnitTesting.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Utilities.UnitTesting.Extensions
{
		public static class ModelProviderExtensions
		{
				public static IEnumerable<MethodInfo> GetClassMethods(this object obj)
				{
						var methods = obj.GetType().GetMethods();

						return methods;
				}

				public static bool Exists(this Assembly assembly, string key)
				{
						var embeddedResources = assembly.GetManifestResourceNames();

						return embeddedResources.Contains(key);
				}

				public static T GetResource<T>(this Assembly assembly, string resourceName)
				{
						var resourceStream = Assembly
								.GetCallingAssembly()
								.GetManifestResourceStream(resourceName);

						using (var reader = new StreamReader(resourceStream))
						{
								var content = reader.ReadToEnd();

								var model = JsonConvert.DeserializeObject<T>(content);

								return model;
						}
				}

				public static IEnumerable<MethodInfo> GetMethods(this object obj)
				{
						var methods = obj
								.GetType()
								.GetMethods();

						return methods;
				}

				public static string GetClassRegistrationKey(this MethodInfo method)
				{
						var registrationAttribute = method.GetCustomAttribute<RegisterModelAttribute>();

						return registrationAttribute?.Key ?? default;
				}

				public static object GetInstance(this Type type)
				{
						return Activator.CreateInstance(type);
				}
		}
}
