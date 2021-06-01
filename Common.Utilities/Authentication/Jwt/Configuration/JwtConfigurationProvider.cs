/* Copyright (C) 2021 Dan Leonard
 * 
 * This  is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */

using Common.Utilities.AspNetCore.Response.Extensions;
using Common.Utilities.Authentication.Extensions;
using Common.Utilities.Authentication.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Common.Utilities.Authentication.Jwt.Configuration
{
		public static class JwtConfigurationProvider
		{
				public static void ConfigureJwtAuthentication(this IServiceCollection serviceDescriptors, IConfiguration configuration)
				{
						var authenticationSettings = configuration.GetInstance<AuthenticationSettings>();

						var validationParameters = GetTokenValidationParameters(authenticationSettings);

						serviceDescriptors.AddAuthentication("Bearer")
								.AddJwtBearer("Bearer", options =>
								{
										options.SaveToken = true;
										options.TokenValidationParameters = validationParameters;
								});
				}

				private static TokenValidationParameters GetTokenValidationParameters(AuthenticationSettings settings)
				{
						var parameters = new TokenValidationParameters
						{
								IssuerSigningKey = settings.GetSecurityKey(),
								RequireAudience = false,
								RequireExpirationTime = true,
								ValidateActor = false,
								ValidateAudience = false,
								ValidateIssuer = false,
								ValidateIssuerSigningKey = true,
								ValidateLifetime = true,
								ValidateTokenReplay = false
						};

						return parameters;
				}
		}
}
