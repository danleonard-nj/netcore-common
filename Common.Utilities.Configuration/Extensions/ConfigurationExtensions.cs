/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.Configuration.Settings;
using Common.Utilities.Configuration.Attributes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities.Configuration.Extensions
{
		public static class ConfigurationExtensions
		{
				public static bool KeyVaultAttributesDefined(this object instance)
				{
						var secretAttributesDefined = instance
								.GetType()
								.GetProperties()
								.Any(property => Attribute.IsDefined(property, typeof(AzureKeyVaultSecretAttribute)));

						return secretAttributesDefined;
				}

				public static void InjectKeyVaultSecrets(this object instance, Dictionary<string, string> keyVault)
				{
						var properties = instance
								.GetType()
								.GetProperties();

						foreach (var property in properties)
						{
								if (Attribute.IsDefined(property, typeof(AzureKeyVaultSecretAttribute)))
								{
										var secretKey = property.Name;

										if (keyVault.ContainsKey(secretKey))
										{
												var secretValue = keyVault.GetValueOrDefault(secretKey);

												property.SetValue(instance, secretValue);
										}
								}
						}
				}

				public static ServiceConfigurationSettings GetServiceConfiguration(this IConfiguration configuration)
				{
						var instance = Activator.CreateInstance<ServiceConfigurationSettings>();
						configuration.GetSection(typeof(ServiceConfigurationSettings).Name).Bind(instance);

						return instance;
				}
		}
}
