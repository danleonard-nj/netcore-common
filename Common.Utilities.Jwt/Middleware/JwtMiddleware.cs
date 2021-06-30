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



using Common.Utilities.Exceptions.Authentication;
using Common.Utilities.Helpers;
using Common.Utilities.Jwt.Extensions;
using Common.Utilities.Jwt.Middleware.Attributes;
using Common.Utilities.Middleware.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Jwt.Middleware
{

		public class JwtMiddleware : ICustomMiddleware
		{
				public JwtMiddleware(RequestDelegate next,
						IJwtTokenProvider jwtTokenProvider)
				{
						_jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
						_next = next ?? throw new ArgumentNullException(nameof(next));
				}

				public async Task Invoke(HttpContext context)
				{
						if (!context.IsEndpointAttributeDefined<BypassAuthenticationAttribute>())
						{
								var token = context.GetBearerToken();

								if (token == null)
								{
										throw new NullTokenException<JwtMiddleware>(Caller.GetMethodName());
								}

								var isAuthenticated = await VerifyToken(token);

								if (!isAuthenticated)
								{
										throw new InvalidTokenException<JwtMiddleware>(Caller.GetMethodName());
								}
						}

						await _next(context);
				}

				private async Task<bool> VerifyToken(string token)
				{
						try
						{
								await _jwtTokenProvider.ValidateToken(token);

								return true;
						}

						catch (Exception)
						{
								return false;
						}
				}

				private readonly RequestDelegate _next;
				private readonly IJwtTokenProvider _jwtTokenProvider;
		}
}
