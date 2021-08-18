/* Copyright (C) 2021 Dan Leonard
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


using Common.Utilities.Configuration.Enums;
using Common.Utilities.Configuration.Extensions;
using Microsoft.Extensions.Configuration;

namespace Common.Utilities.AspNetCore
{
		public class AspNetCoreConfigurationOptions
		{
				// If the service configuration is coming from IConfiguration

				public AspNetCoreConfigurationOptions(IConfiguration configuration)
				{
						var serviceConfigurationSettings = configuration.GetServiceConfiguration();

						InjectAzureKeyVaultSecrets = serviceConfigurationSettings.InjectAzureKeyVaultSecrets;
						AzureKeyVaultUri = serviceConfigurationSettings.AzureKeyVaultUri;
				}

				public AspNetCoreConfigurationOptions()
				{
				}

				public bool InjectAzureKeyVaultSecrets { get; set; } = false;
				public string AzureKeyVaultUri { get; set; }
				public HostEnvironment Environment { get; set; }
		}
}
