/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
