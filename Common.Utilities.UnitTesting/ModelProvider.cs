/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Extensions.Collections;
using Common.Utilities.UnitTesting.Attributes;
using Common.Utilities.UnitTesting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Utilities.UnitTesting
{
		public class ModelProvider
		{
				public ModelProvider()
				{
						_assembly = Assembly.GetCallingAssembly();

						_resourceNames = GetEmbeddedResourceNames();
						_classRegistrations = new Dictionary<string, object>();
				}

				public ModelProvider(IEnumerable<Type> classRegistrations)
				{
						_assembly = Assembly.GetCallingAssembly();

						_resourceNames = GetEmbeddedResourceNames();
						_classRegistrations = GetClassRegistrations(classRegistrations);
				}

				public T Get<T>()
				{
						var key = typeof(T).Name;
								
						var model = Get<T>(key);

						return model;
				}

				public T Get<T>(string key)
				{
						if (_resourceNames.ContainsKey(key))
						{
								var resourceName = _resourceNames[key];
								var resource = _assembly.GetResource<T>(resourceName);

								return resource;
						}

						if (_classRegistrations.ContainsKey(key))
						{
								return (T)_classRegistrations[key];
						}

						return default;
				}

				private Dictionary<string, string> GetEmbeddedResourceNames()
				{
						var embeddedResources = Assembly
								.GetCallingAssembly()
								.GetManifestResourceNames();

						var embeddedResourceDefinitions = embeddedResources.ToDictionary(
								a => a.Split(".").TakeLast(2).FirstOrDefault(),
								b => b);

						return embeddedResourceDefinitions;
				}

				private Dictionary<string, object> GetClassRegistrations(IEnumerable<Type> types)
				{
						var models = new Dictionary<string, object>();

						foreach (var registration in types)
						{
								var methodInfo = registration.GetMethods()
										.Where(x => Attribute.IsDefined(x, typeof(RegisterModelAttribute)));

								var objects = methodInfo.ToDictionary(
										a => a.GetClassRegistrationKey() ?? a.Name.Replace("Get", ""),
										b => b.Invoke(registration.GetInstance(), null));

								models.AddRange(objects);
						}

						return models;
				}

				private readonly Assembly _assembly;
				private readonly Dictionary<string, string> _resourceNames;
				private readonly Dictionary<string, object> _classRegistrations;
		}
}
