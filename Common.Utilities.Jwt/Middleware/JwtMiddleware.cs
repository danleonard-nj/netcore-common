/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Helpers;
using Common.Utilities.Jwt.Extensions;
using Common.Utilities.Jwt.Middleware.Attributes;
using Common.Utilities.Middleware.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Authentication;
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
										throw new AuthenticationException($"{Caller.GetMethodName()}: No Bearer supplied in request.");
								}

								var isAuthenticated = await VerifyToken(token);

								if (!isAuthenticated)
								{
										throw new AuthenticationException($"{Caller.GetMethodName()}: Invalid Bearer token.");
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
