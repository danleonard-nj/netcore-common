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


using Microsoft.IdentityModel.Tokens;
using System;

namespace Common.Utilities.Jwt
{
		public class JwtTokenProviderOptions
		{
				public int TokenLifetime { get; set; } = 5;
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
								ValidateIssuerSigningKey = false,
								ValidateIssuer = false,
								ValidateAudience = false,
								ClockSkew = TimeSpan.Zero
						};
		}
}
