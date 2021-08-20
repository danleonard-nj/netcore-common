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


using Common.Models.Jwt;
using Common.Models.Jwt.Abstractions;
using Common.Models.Jwt.Settings;
using Common.Utilities.Helpers;
using Common.Utilities.Jwt.Dependencies.Providers;
using Common.Utilities.Jwt.Encryption;
using Common.Utilities.Jwt.Extensions;
using JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Authentication;
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
				public int TokenLifetime { get => _jwtTokenProviderOptions.TokenLifetime; }

				private readonly byte[] _publicKey;
				private readonly int _tokenLifetime;

				public JwtTokenProvider(IJwtDependencyProvider jwtDependencyProvider)
				{
						if (jwtDependencyProvider == null)
						{
								throw new ArgumentNullException(nameof(jwtDependencyProvider));
						}

						_jwtTokenDecoder = jwtDependencyProvider.GetDecoder();
						_jwtTokenEncoder = jwtDependencyProvider.GetEncoder();
						_securityTokenValidator = jwtDependencyProvider.GetSecurityTokenValidator();
						_jwtTokenProviderOptions = jwtDependencyProvider.GetJwtTokenProviderOptions();
						_jwtAuthenticationSettings = jwtDependencyProvider.GetJwtAuthenticationSettings();

						var publicKey = _jwtTokenProviderOptions?.PublicKey ?? _jwtAuthenticationSettings?.PublicKey;
						
						if (publicKey == default)
						{
								throw new Exception($"{GetType()}: {Caller.GetMethodName()}: No public key found.");
						}

						_publicKey = publicKey.Encode();
						_jwtTokenProviderOptions.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(_publicKey);

						// Coalesce token lifetime configuration values, default to 60;

						_tokenLifetime = _jwtTokenProviderOptions?.TokenLifetime 
								?? _jwtAuthenticationSettings?.TokenLifetime ?? TOKEN_LIFETIME_DEFAULT;
				}

				public async Task<string> GetToken(IJwtPayload jwtPayload)
				{
						var token = await _jwtTokenEncoder.GetEncodedToken(
								jwtPayload, _publicKey);

						return token;
				}

				public async Task<string> GetToken(string token)
				{
						if (await ValidateToken(token))
						{
								var payload = await GetPayload(token);

								payload.Refresh(_tokenLifetime);

								var newToken = await GetToken(payload);

								return newToken;
						}

						else
						{
								throw new AuthenticationException($"{Caller.GetMethodName()}: Invalid Bearer token.");
						}
				}

				public async Task<JwtPayload> GetPayload(string token)
				{
						var payload = await _jwtTokenDecoder.GetDecodedToken(token, _publicKey);

						return payload;
				}

				public async Task<T> GetPayload<T>(string token)
				{
						var payload = await _jwtTokenDecoder.GetDecodedToken<T>(token, _publicKey);

						return payload;
				}

				public async Task<bool> ValidateToken(string token)
				{
						await Task.Yield();

						var validationParameters = _jwtTokenProviderOptions.TokenValidationParameters;


						try
						{
								validationParameters.IssuerSigningKey = new SymmetricSecurityKey(_publicKey);

								_securityTokenValidator.ValidateToken(
										token, validationParameters, out var validatedToken);

								return true;
						}

						catch (Exception ex)
						{
								throw new AuthenticationException($"{Caller.GetMethodName()}: Failed to validate Bearer token.");
						}
				}

				private IJwtTokenDecoder _jwtTokenDecoder;
				private IJwtTokenEncoder _jwtTokenEncoder;
				private ISecurityTokenValidator _securityTokenValidator;
				private JwtTokenProviderOptions _jwtTokenProviderOptions;
				private JwtAuthenticationSettings _jwtAuthenticationSettings;

				private const int TOKEN_LIFETIME_DEFAULT = 60;
		}
}
