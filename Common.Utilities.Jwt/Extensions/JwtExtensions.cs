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


using Common.Models.Jwt.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Linq;
using System.Text;

namespace Common.Utilities.Jwt.Extensions
{
		public static class JwtExtensions
		{
				public static byte[] Encode(this string key)
				{
						var encoded = Encoding.UTF8.GetBytes(key);

						return encoded;
				}

				public static IJwtPayload Refresh(this IJwtPayload payload, int tokenLifetime)
				{
						var expiry = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime).ToUnixTimeSeconds();

						payload.ExpiresOn = expiry;

						return payload;
				}

				public static string GetBearerToken(this HttpContext context)
				{
						var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

						var token = authorizationHeader?.Split(" ")?.LastOrDefault();

						return token;
				}

				public static bool IsEndpointAttributeDefined<T>(this HttpContext context) where T : Attribute
				{
						var attribute = context
								.Features
								.Get<IEndpointFeature>()?
								.Endpoint?
								.Metadata?
								.GetMetadata<T>();

						if (attribute != null)
						{
								return true;
						}

						return false;
				}
		}
}
