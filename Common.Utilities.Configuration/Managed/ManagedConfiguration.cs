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
using Common.Models.Configuration.Settings;
using Common.Utilities.Exceptions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Common.Utilities.Configuration.Managed
{
		public interface IManagedConfiguration
		{
				bool IsKeyVault { get; }

				void BuildAzureKeyVaultConfiguration();
				void BuildDefaultConfiguration();
				IConfiguration GetConfiguration();
		}

		public class ManagedConfiguration : IManagedConfiguration
		{
				public bool IsKeyVault { get; private set; }

				public ManagedConfiguration(IHostEnvironment hostEnvironment, bool injectKeyVaultSecrets = false)
				{
						_hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));

						_configuration = default;

						if (!_hostEnvironment.IsDevelopment() || injectKeyVaultSecrets)
						{
								IsKeyVault = true;
						}
				}

				public IConfiguration GetConfiguration()
				{
						if (_configuration == default)
						{
								throw new ManagedConfigurationException("Configuration must be built before it can be retrieved.");
						}

						return _configuration;
				}

				public void BuildAzureKeyVaultConfiguration()
				{
						var builder = new ConfigurationBuilder();

						var uri = GetAzureKeyVaultUri(_hostEnvironment);

						var secretClient = new SecretClient(new Uri(uri), new DefaultAzureCredential());

						builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

						_configuration = builder.Build();
				}

				public void BuildDefaultConfiguration()
				{
						var builder = new ConfigurationBuilder();

						builder.AddEnvironmentVariables();

						builder.AddJsonFile(GetJsonConfigurationName(_hostEnvironment));

						_configuration = builder.Build();
				}

				private string GetJsonConfigurationName(IHostEnvironment hostEnvironment)
				{
						return hostEnvironment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
				}

				private string GetAzureKeyVaultUri(IHostEnvironment hostEnvironment)
				{
						var configuration = new ConfigurationBuilder()
								.AddJsonFile(GetJsonConfigurationName(hostEnvironment))
								.Build();

						var keyVaultSettings = Activator.CreateInstance<AzureKeyVaultConfigurationSettings>();
						configuration.GetSection(typeof(AzureKeyVaultConfigurationSettings).Name).Bind(keyVaultSettings);

						return keyVaultSettings?.AzureKeyVaultUri;
				}

				private readonly IHostEnvironment _hostEnvironment;
				private IConfiguration _configuration;
		}
}
