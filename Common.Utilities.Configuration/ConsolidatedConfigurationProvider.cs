/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Common.Utilities.Configuration.Enums;
using Common.Utilities.Configuration.Extensions;
using Common.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Utilities.Configuration.Binding
{
		public interface IConsolidatedConfigurationProvider
		{
				IConfiguration DefaultConfiguration { get; }
				bool IsDevelopment { get; }
				bool IsProduction { get; }
				Dictionary<string, string> KeyVault { get; }

				object BindInstance(Type type);
				T BindInstance<T>() where T : class;
		}

		public class ConsolidatedConfigurationProvider : IConsolidatedConfigurationProvider
		{
				// Singleton

				private readonly IConfiguration _defaultConfiguration;
				private readonly Dictionary<string, string> _keyVault;
				private readonly string _keyVaultUri;

				private readonly bool _useKeyvault = false;
				private readonly HostEnvironment _hostEnvironment;

				public IConfiguration DefaultConfiguration { get => _defaultConfiguration; }

				public Dictionary<string, string> KeyVault { get => _keyVault; }

				public bool IsDevelopment { get => _hostEnvironment == HostEnvironment.Development; }

				public bool IsProduction { get => _hostEnvironment == HostEnvironment.Production; }


				public ConsolidatedConfigurationProvider(string keyVaultUri = default, HostEnvironment environment = HostEnvironment.Development)
				{
						_defaultConfiguration = BuildDefaultConfiguration();

						var serviceConfiguration = _defaultConfiguration.GetServiceConfiguration();

						if ((keyVaultUri ?? serviceConfiguration?.AzureKeyVaultUri) != default)
						{
								_useKeyvault = serviceConfiguration?.InjectAzureKeyVaultSecrets ?? false;
								_keyVaultUri = serviceConfiguration?.AzureKeyVaultUri ?? keyVaultUri;
								_keyVault = GetKeyVaultSecrets();
						}

						_hostEnvironment = environment;
				}

				public T BindInstance<T>() where T : class
				{
						var instance = BindInstance(typeof(T));

						return (T)instance;
				}

				public object BindInstance(Type type)
				{
						var instance = Activator.CreateInstance(type);

						_defaultConfiguration.GetSection(type.Name).Bind(instance);

						if (instance.KeyVaultAttributesDefined() && _useKeyvault)
						{
								instance.InjectKeyVaultSecrets(_keyVault);
						}

						return instance;
				}

				private IConfiguration BuildDefaultConfiguration()
				{
						try
						{
								var configuration = new ConfigurationBuilder()
								.AddJsonFile(_hostEnvironment == HostEnvironment.Development ? DEV_APP_SETTINGS : DEF_APP_SETTINGS)
								.Build();

								return configuration;
						}

						catch (Exception ex)
						{
								throw new Exception($"{GetType()}: {Caller.GetMethodName()}: Failed to build default configuration: {ex.Message}");
						}
				}

				private async Task<Dictionary<string, string>> GetKeyVaultSecretsAsync()
				{
						var client = GetAzureKeyVaultSecretClient();

						var secrets = new Dictionary<string, string>();

						await foreach (var property in client.GetPropertiesOfSecretsAsync())
						{
								var response = await client.GetSecretAsync(property.Name);
								var secret = response?.Value;

								if (secret != default)
								{
										secrets.Add(property.Name, secret.Value);
								}
						}

						return secrets;
				}

				private Dictionary<string, string> GetKeyVaultSecrets()
				{
						var keyVault = Task.Run(async () => await GetKeyVaultSecretsAsync()).Result;

						return keyVault;
				}

				private SecretClient GetAzureKeyVaultSecretClient()
				{
						try
						{
								var azureCredential = new DefaultAzureCredential();

								var secretClient = new SecretClient(new Uri(_keyVaultUri), azureCredential);

								return secretClient;
						}

						catch (Exception ex)
						{
								// TODO: Exception type here?

								throw new Exception($"{GetType()}: {Caller.GetMethodName()}: Failed to create Azure KeyVault secret client: {ex.Message}");
						}
				}

				private const string DEF_APP_SETTINGS = "appsettings.json";
				private const string DEV_APP_SETTINGS = "appsettings.Development.json";
		}
}
