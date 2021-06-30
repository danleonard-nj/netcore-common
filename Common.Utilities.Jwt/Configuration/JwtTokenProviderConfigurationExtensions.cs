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


using Common.Models.Jwt.Settings;
using Common.Utilities.Jwt.Dependencies.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Utilities.Jwt.Configuration
{
		public static class JwtTokenProviderConfigurationExtensions
		{
				public static JwtTokenProvider ConfigureJwtTokenProvider(this ServiceProvider provider, JwtTokenProviderOptions jwtOptions)
				{
						var jwtDependencies = provider.GetService<IJwtDependencyProvider>();

						var tokenProvider = new JwtTokenProvider(jwtDependencies, jwtOptions);

						return tokenProvider;
				}

				public static JwtTokenProviderOptions ConfigureJwtTokenProviderOptions(this ServiceProvider provider, JwtTokenProviderOptions jwtOptions)
				{
						if (!jwtOptions.IsSecurityKeySet)
						{
								var jwtSettings = provider.GetService<JwtSettings>();

								if (jwtSettings.PublicKey == null)
								{
										throw new ArgumentNullException("No public key definition found.");
								}

								jwtOptions.SetSecurityKey(jwtSettings.PublicKey);
						}

						return jwtOptions;
				}
		}
}
