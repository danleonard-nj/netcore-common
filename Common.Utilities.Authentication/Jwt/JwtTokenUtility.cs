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
using JWT;
using System;

namespace Common.Utilities.Authentication.Jwt
{
		public interface IJwtTokenUtility
		{
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
						_publicKey = jwtUtilityFactory.GetEncodedPublicKey();
				}

				public string GetToken(IUserModel user)
				{
						var payload = new JwtPayload
						{
								UserId = user.UserId,
								EmailAddress = user.Email,
								Username = user.Username,
								ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()
						};

						var token = _jwtEncoder.Encode(payload, _publicKey);

						return token;
				}

				private readonly byte[] _publicKey;
				private readonly IJwtEncoder _jwtEncoder;
		}
}
