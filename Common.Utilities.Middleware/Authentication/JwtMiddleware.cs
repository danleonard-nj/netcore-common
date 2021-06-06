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

using Common.Utilities.Authentication.Attributes;
using Common.Utilities.Authentication.Extensions;
using Common.Utilities.Authentication.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.Authentication
{

		public class JwtMiddleware : ICustomMiddleware
		{
				public JwtMiddleware(RequestDelegate next,
						IJwtDependencyProvider jwtDependencyProvider)
				{
						if (next == null)
						{
								throw new ArgumentNullException(nameof(next));
						}

						_next = next;

						if (jwtDependencyProvider == null)
						{
								throw new ArgumentNullException(nameof(jwtDependencyProvider));
						}

						_securityTokenValidator = jwtDependencyProvider.GetTokenHandler();
						_tokenValidationParameters = jwtDependencyProvider.GetTokenValidationParameters();

				}
				public async Task Invoke(HttpContext context)
				{
						if (!context.IsEndpointAttributeDefined<BypassAuthenticationAttribute>())
						{
								var token = context.GetBearerToken();

								if (token == null)
								{
										throw new AuthenticationException("No bearer token supplied in request.");
								}

								var isAuthenticated = VerifyToken(token);

								if (!isAuthenticated)
								{
										throw new AuthenticationException("Unauthorized.");
								}
						}

						await _next(context);
				}

				private bool VerifyToken(string token)
				{
						try
						{
								_securityTokenValidator.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

								return true;
						}

						catch (Exception)
						{
								return false;
						}
				}

				private readonly RequestDelegate _next;
				private readonly TokenValidationParameters _tokenValidationParameters;
				private readonly ISecurityTokenValidator _securityTokenValidator;
		}
}
