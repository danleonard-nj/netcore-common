/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Configuration.Attributes;

namespace Common.Models.Jwt.Settings
{
		public class JwtAuthenticationSettings
		{
				[AzureKeyVaultSecret]
				public string PublicKey { get; set; }
				public int TokenLifetime { get; set; }
		}
}
