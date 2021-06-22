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

using Common.Models.Authentication.Jwt;
using Common.Models.Authentication.User.Abstractions;
using Common.Utilities.Authentication.Extensions;
using JWT;
using Newtonsoft.Json;
using System;

namespace Common.Utilities.Authentication.Jwt
{
		public interface IJwtTokenUtility
		{
				JwtPayload GetPayload(string token);
				string GetRefreshToken(string token);
				string GetToken(IUserModel user);
		}

		public class JwtTokenUtility : IJwtTokenUtility
		{
				public JwtTokenUtility(IJwtDependencyProvider jwtUtilityFactory)
				{
						if (jwtUtilityFactory == null)
						{
								throw new ArgumentNullException(nameof(jwtUtilityFactory));
						}

						_jwtEncoder = jwtUtilityFactory.GetEncoder();

						if (_jwtEncoder == null)
						{
								throw new ArgumentNullException(nameof(_jwtEncoder));
						}

						_jwtDecoder = jwtUtilityFactory.GetDecoder();

						if (_jwtDecoder == null)
						{
								throw new ArgumentNullException(nameof(_jwtDecoder));
						}

						_publicKey = jwtUtilityFactory.GetEncodedPublicKey();

						if (_publicKey == null)
						{
								throw new ArgumentNullException(nameof(_publicKey));
						}
				}

				public string GetToken(IUserModel user)
				{
						var payload = user.ToJwtPayload(5);

						var token = _jwtEncoder.Encode(payload, _publicKey);

						return token;
				}

				public JwtPayload GetPayload(string token)
				{
						var payloadJson = _jwtDecoder.Decode(token, _publicKey, true);

						var payload = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);

						return payload;
				}

				public string GetRefreshToken(string token)
				{
						var payload = GetPayload(token);

						var user = payload.ToUserModel();

						var refreshToken = GetToken(user);

						return refreshToken;
				}

				private readonly byte[] _publicKey;
				private readonly IJwtEncoder _jwtEncoder;
				private readonly IJwtDecoder _jwtDecoder;
		}
}
