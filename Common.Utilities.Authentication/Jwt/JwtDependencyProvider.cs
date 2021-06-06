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

using Common.Utilities.Authentication.Extensions;
using Common.Utilities.Authentication.Settings;
using JWT;
using JWT.Algorithms;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Common.Utilities.Authentication.Jwt
{
		public interface IJwtDependencyProvider
		{
				IJwtDecoder GetDecoder();
				byte[] GetEncodedPublicKey();
				IJwtEncoder GetEncoder();
				string GetPublicKey();
				SymmetricSecurityKey GetSecurityKey();
				JwtSecurityTokenHandler GetTokenHandler();
				TokenValidationParameters GetTokenValidationParameters();
				IJwtValidator GetValidator();
		}

		public class JwtDependencyProvider : IJwtDependencyProvider
		{
				public JwtDependencyProvider(IJsonSerializer jsonSerializer,
						IBase64UrlEncoder urlEncoder,
						IJwtAlgorithm jwtAlgorithm,
						IDateTimeProvider dateTimeProvider,
						AuthenticationSettings settings)
				{
						_jwtAlgorithm = jwtAlgorithm ?? throw new ArgumentNullException(nameof(jwtAlgorithm));
						_urlEncoder = urlEncoder ?? throw new ArgumentNullException(nameof(urlEncoder));
						_jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(urlEncoder));
						_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
						_settings = settings ?? throw new ArgumentNullException(nameof(settings));
				}

				public IJwtEncoder GetEncoder()
				{
						return new JwtEncoder(_jwtAlgorithm, _jsonSerializer, _urlEncoder);
				}

				public IJwtDecoder GetDecoder()
				{
						return new JwtDecoder(_jsonSerializer, _urlEncoder);
				}

				public IJwtValidator GetValidator()
				{
						return new JwtValidator(_jsonSerializer, _dateTimeProvider);
				}

				public JwtSecurityTokenHandler GetTokenHandler()
				{
						return new JwtSecurityTokenHandler();
				}

				public byte[] GetEncodedPublicKey()
				{
						var key = _settings.GetEncodedPublicKey();

						return key;
				}

				public SymmetricSecurityKey GetSecurityKey()
				{
						var securityKey = _settings.GetSecurityKey();

						return securityKey;
				}

				public string GetPublicKey()
				{
						var key = _settings.PublicKey;

						return key;
				}

				public TokenValidationParameters GetTokenValidationParameters()
				{
						var securityKey = GetSecurityKey();

						var parameters = new TokenValidationParameters
						{
								ValidateIssuerSigningKey = true,
								IssuerSigningKey = securityKey,
								ValidateIssuer = false,
								ValidateAudience = false,
								ClockSkew = TimeSpan.Zero
						};

						return parameters;
				}

				private readonly AuthenticationSettings _settings;
				private readonly IJsonSerializer _jsonSerializer;
				private readonly IBase64UrlEncoder _urlEncoder;
				private readonly IJwtAlgorithm _jwtAlgorithm;
				private readonly IDateTimeProvider _dateTimeProvider;

		}
}
