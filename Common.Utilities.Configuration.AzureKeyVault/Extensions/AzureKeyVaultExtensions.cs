/* Copyright (C) 2012, 2013 Dan Leonard
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


using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Common.Utilities.Collections;
using Common.Utilities.Configuration.AzureKeyVault.Attributes;
using Common.Utilities.Exceptions.Configuration;
using Common.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Common.Utilities.Configuration.AzureKeyVault.Extensions
{
		public static class AzureKeyVaultExtensions
		{
				public static void InjectKeyVaultVariables(this object configuration, IConfiguration keyVaultConfiguration)
				{
						if (configuration.KeyVaultValuesDefined())
						{
								var keyVaultKeys = keyVaultConfiguration
										.AsEnumerable()
										.Select(x => x.Key);

								var properties = configuration
										.GetType()
										.GetProperties();

								var keyVaultProperties = properties
										.Where(property => Attribute.IsDefined(property, typeof(KeyVaultSecretAttribute)));

								foreach (var property in keyVaultProperties)
								{
										var keyVaultKey = property.Name;
										
										if (!keyVaultKeys.Contains(keyVaultKey))
										{
												throw new AzureKeyVaultConfigurationException($"{typeof(AzureKeyVaultExtensions).Name}: {Caller.GetMethodName()}: {keyVaultKey} not found in key vault.");
										}

										var keyVaultValue = keyVaultConfiguration[keyVaultKey];

										property.SetValue(configuration, keyVaultValue);
								}
						}

						else
						{
								throw new AzureKeyVaultConfigurationException($"{typeof(AzureKeyVaultExtensions).Name}: {Caller.GetMethodName()}: No key vault attributes defined on target object.");
						}
				}

				public static bool KeyVaultValuesDefined(this object obj)
				{
						var attributes = obj
								.GetType()
								.GetProperties()
								.Where(property => Attribute.IsDefined(property, typeof(KeyVaultSecretAttribute)));

						if (attributes.Any())
						{
								return true;
						}

						return false;
				}

				public static IConfiguration GetAzureKeyVaultConfiguration(this IConfiguration configuration, string uriKey = "AzureKeyVaultUri")
				{
						var builder = new ConfigurationBuilder();

						var keyVaultUri = configuration.GetAzureKeyVaultUri(uriKey);

						var secretClient = new SecretClient(
								new Uri(keyVaultUri),
								new DefaultAzureCredential());

						builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

						return builder.Build();
				}

				private static string GetAzureKeyVaultUri(this IConfiguration configuration, string key)
				{
						var pairs = configuration
								.AsEnumerable()
								.Select(x => new Pair<string, string>
								{
										Key = x.Key,
										Value = x.Value
								});

						var keyVaultUrl = pairs
								.Where(x => x.Key.Contains(key))
								.FirstOrDefault();

						if (keyVaultUrl?.Value == null)
						{
								throw new AzureKeyVaultConfigurationException($"{typeof(AzureKeyVaultExtensions).Name}: {Caller.GetMethodName()}: URI is not defined.");
						}

						return keyVaultUrl?.Value;
				}
		}
}
