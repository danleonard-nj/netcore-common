/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Microsoft.IdentityModel.Tokens;
using System;

namespace Common.Utilities.Jwt
{
		public class JwtTokenProviderOptions
		{
				public int TokenLifetime { get; set; }
				public string PublicKey { get; set; }
				public TokenValidationParameters TokenValidationParameters { get; private set; }

				public JwtTokenProviderOptions(TokenValidationParameters validationParameters = default)
				{
						TokenValidationParameters = validationParameters ?? _defaultValidationParameters;
				}

				//public void SetSecurityKey(string key)
				//{
				//		var securityKey = new SymmetricSecurityKey(key.Encode());
				//		TokenValidationParameters.IssuerSigningKey = securityKey;
				//}

				private readonly TokenValidationParameters _defaultValidationParameters =
						new TokenValidationParameters
						{
								ValidateIssuerSigningKey = true,
								ValidateIssuer = false,
								ValidateAudience = false,
								ClockSkew = TimeSpan.Zero
						};
		}
}
