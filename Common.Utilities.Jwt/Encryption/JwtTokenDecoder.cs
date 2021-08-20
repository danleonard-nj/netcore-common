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
using JWT;
using JWT.Algorithms;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Jwt.Encryption
{
		public interface IJwtTokenDecoder
		{
				Task<JwtPayload> GetDecodedToken(string token, byte[] encryptionKey);
				Task<T> GetDecodedToken<T>(string token, byte[] encryptionKey);
		}

		public class JwtTokenDecoder : IJwtTokenDecoder
		{
				public JwtTokenDecoder(IJsonSerializer jsonSerializer,
						IBase64UrlEncoder base64UrlEncoder,
						IJwtAlgorithm jwtAlgorithm,
						IDateTimeProvider dateTimeProvider)
				{

						if (jsonSerializer == null)
						{
								throw new ArgumentNullException(nameof(jsonSerializer));
						}

						if (base64UrlEncoder == null)
						{
								throw new ArgumentNullException(nameof(base64UrlEncoder));
						}

						if (jwtAlgorithm == null)
						{
								throw new ArgumentNullException(nameof(jwtAlgorithm));
						}

						var jwtValidator = new JwtValidator(jsonSerializer, dateTimeProvider);

						_jwtDecoder = new JwtDecoder(jsonSerializer, jwtValidator, base64UrlEncoder, jwtAlgorithm);
				}

				public async Task<JwtPayload> GetDecodedToken(string token, byte[] encryptionKey)
				{
						await Task.Yield();

						var decoded = _jwtDecoder.DecodeToObject<JwtPayload>(token, encryptionKey, true);

						return decoded;
				}

				public async Task<T> GetDecodedToken<T>(string token, byte[] encryptionKey)
				{
						await Task.Yield();

						var decoded = _jwtDecoder.DecodeToObject<T>(token, encryptionKey, true);

						return decoded;
				}

				private readonly IJwtDecoder _jwtDecoder;
		}
}
