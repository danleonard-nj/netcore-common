using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.AspNetCore.Options
{
		public class ConfigureAspNetCoreServicesOptions
		{
				public bool InjectKeyVaultSecrets { get; set; } = false;
		}
}
