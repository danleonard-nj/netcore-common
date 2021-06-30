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


using Common.Utilities.Jwt.Encryption;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Common.Utilities.Jwt.Dependencies.Providers
{
		public interface IJwtDependencyProvider
		{
				IJwtTokenDecoder GetDecoder();
				IJwtTokenEncoder GetEncoder();
				ISecurityTokenValidator GetSecurityTokenValidator();
		}

		public class JwtDependencyProvider : IJwtDependencyProvider
		{
				public JwtDependencyProvider(IJwtTokenDecoder jwtTokenDecoder,
						IJwtTokenEncoder jwtTokenEncoder,
						ISecurityTokenValidator securityTokenValidator)
				{
						_securityTokenValidator = securityTokenValidator ?? throw new ArgumentNullException(nameof(securityTokenValidator));
						_jwtTokenDecoder = jwtTokenDecoder ?? throw new ArgumentNullException(nameof(jwtTokenDecoder));
						_jwtTokenEncoder = jwtTokenEncoder ?? throw new ArgumentNullException(nameof(jwtTokenEncoder));
				}

				public IJwtTokenEncoder GetEncoder()
				{
						return _jwtTokenEncoder;
				}

				public IJwtTokenDecoder GetDecoder()
				{
						return _jwtTokenDecoder;
				}

				public ISecurityTokenValidator GetSecurityTokenValidator()
				{
						return _securityTokenValidator;
				}

				private readonly ISecurityTokenValidator _securityTokenValidator;
				private readonly IJwtTokenEncoder _jwtTokenEncoder;
				private readonly IJwtTokenDecoder _jwtTokenDecoder;
		}
}
