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


using Common.Models.Jwt;
using Common.Models.Jwt.Abstractions;
using Common.Utilities.Exceptions.Authentication;
using Common.Utilities.Helpers;
using Common.Utilities.Jwt.Dependencies.Providers;
using Common.Utilities.Jwt.Encryption;
using Common.Utilities.Jwt.Extensions;
using JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Jwt
{
		public interface IJwtTokenProvider
		{
				int TokenLifetime { get; }

				Task<JwtPayload> GetPayload(string token);
				Task<string> GetToken(IJwtPayload jwtPayload);
				Task<string> GetToken(string token);
				Task<bool> ValidateToken(string token);
		}

		public class JwtTokenProvider : IJwtTokenProvider
		{
				public int TokenLifetime { get => _options.TokenLifetime; }

				public JwtTokenProvider(IJwtDependencyProvider jwtDependencyProvider,
						JwtTokenProviderOptions options)
				{
						if (jwtDependencyProvider == null)
						{
								throw new ArgumentNullException(nameof(jwtDependencyProvider));
						}

						_jwtTokenDecoder = jwtDependencyProvider.GetDecoder();
						_jwtTokenEncoder = jwtDependencyProvider.GetEncoder();
						_securityTokenValidator = jwtDependencyProvider.GetSecurityTokenValidator();

						_options = options ?? throw new ArgumentNullException(nameof(options));
				}

				public async Task<string> GetToken(IJwtPayload jwtPayload)
				{
						var token = await _jwtTokenEncoder.GetEncodedToken(jwtPayload, _options.SecurityKey.Encode());

						return token;
				}

				public async Task<string> GetToken(string token)
				{
						if (await ValidateToken(token))
						{
								var payload = await GetPayload(token);

								payload.Refresh(_options.TokenLifetime);

								var newToken = await GetToken(payload);

								return newToken;
						}

						else
						{
								throw new InvalidTokenException<JwtTokenProvider>(Caller.GetMethodName());
						}
				}

				public async Task<JwtPayload> GetPayload(string token)
				{
						var payload = await _jwtTokenDecoder.GetDecodedToken(token, _options.SecurityKey.Encode());

						return payload;
				}

				public async Task<bool> ValidateToken(string token)
				{
						await Task.Yield();

						var validationParameters = _options.TokenValidationParameters;

						try
						{
								_options.TokenValidationParameters.ValidateIssuerSigningKey = false;

								_securityTokenValidator.ValidateToken(token, validationParameters, out var validatedToken);

								return true;
						}

						catch (Exception ex)
						{
								throw new InvalidTokenException<JwtTokenProvider>(Caller.GetMethodName());
						}
				}

				private IJwtTokenDecoder _jwtTokenDecoder;
				private IJwtTokenEncoder _jwtTokenEncoder;
				private ISecurityTokenValidator _securityTokenValidator;
				private JwtTokenProviderOptions _options;
		}
}
