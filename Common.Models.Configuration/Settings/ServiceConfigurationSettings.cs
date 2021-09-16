/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Models.Configuration.Settings
{
		public class ServiceConfigurationSettings
		{
				public string AzureKeyVaultUri { get; set; }
				public bool InjectAzureKeyVaultSecrets { get; set; } = false;

		}
}
