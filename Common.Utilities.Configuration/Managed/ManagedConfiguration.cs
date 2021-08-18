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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Common.Utilities.Configuration.Managed
{
		public interface IManagedConfiguration
		{
				public bool InjectKeyVaultSecrets { get; set; }

				IConfiguration BuildAzureKeyVaultConfiguration();
				IConfiguration BuildDefaultConfiguration();
		}

		public class ManagedConfiguration
		{
				public bool InjectKeyVaultSecrets { get; set; }
				public bool IsDevelopment { get; }

				// Handle the environment from here, assume it's development unless otherwise specified and
				// fetch appsettings.Development.json

				public ManagedConfiguration(bool isDevelopment = true)
				{
						IsDevelopment = isDevelopment;
				}

				public IConfiguration BuildAzureKeyVaultConfiguration()
				{
						var builder = new ConfigurationBuilder();

						var uri = GetAzureKeyVaultUri(_hostEnvironment);

						var secretClient = new SecretClient(new Uri(uri), new DefaultAzureCredential());

						builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

						return builder.Build();
				}

				public IConfiguration BuildDefaultConfiguration()
				{
						var builder = new ConfigurationBuilder();

						builder.AddEnvironmentVariables();

						builder.AddJsonFile(GetJsonConfigurationName(_hostEnvironment));

						return builder.Build();
				}

				private string GetJsonConfigurationName(IHostEnvironment hostEnvironment)
				{
						return hostEnvironment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
				}

				private string GetAzureKeyVaultUri(IHostEnvironment hostEnvironment)
				{
						var defaultConfiguration = BuildDefaultConfiguration();

						var keyVaultSettings = Activator.CreateInstance<AzureKeyVaultConfigurationSettings>();
						defaultConfiguration.GetSection(typeof(AzureKeyVaultConfigurationSettings).Name).Bind(keyVaultSettings);

						return keyVaultSettings?.AzureKeyVaultUri;
				}

				private readonly IHostEnvironment _hostEnvironment;
		}
}
