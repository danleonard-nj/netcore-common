/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.Jwt.Settings;
using Common.Utilities.Jwt.Encryption;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Common.Utilities.Jwt.Dependencies.Providers
{
		public interface IJwtDependencyProvider
		{
				IJwtTokenDecoder GetDecoder();
				IJwtTokenEncoder GetEncoder();
				JwtAuthenticationSettings GetJwtAuthenticationSettings();
				JwtTokenProviderOptions GetJwtTokenProviderOptions();
				ISecurityTokenValidator GetSecurityTokenValidator();
		}

		public class JwtDependencyProvider : IJwtDependencyProvider
		{
				public JwtDependencyProvider(IJwtTokenDecoder jwtTokenDecoder,
						IJwtTokenEncoder jwtTokenEncoder,
						ISecurityTokenValidator securityTokenValidator,
						JwtAuthenticationSettings jwtAuthenticationSettings,
						JwtTokenProviderOptions jwtTokenProviderOptions)
				{
						_securityTokenValidator = securityTokenValidator ?? throw new ArgumentNullException(nameof(securityTokenValidator));
						_jwtTokenDecoder = jwtTokenDecoder ?? throw new ArgumentNullException(nameof(jwtTokenDecoder));
						_jwtTokenEncoder = jwtTokenEncoder ?? throw new ArgumentNullException(nameof(jwtTokenEncoder));
						_jwtTokenProviderOptions = jwtTokenProviderOptions ?? throw new ArgumentNullException(nameof(jwtTokenProviderOptions));
						_jwtAuthenticationSettings = jwtAuthenticationSettings ?? throw new ArgumentNullException(nameof(jwtAuthenticationSettings));
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

				public JwtTokenProviderOptions GetJwtTokenProviderOptions()
				{
						return _jwtTokenProviderOptions;
				}

				public JwtAuthenticationSettings GetJwtAuthenticationSettings()
				{
						return _jwtAuthenticationSettings;
				}

				private readonly ISecurityTokenValidator _securityTokenValidator;
				private readonly IJwtTokenEncoder _jwtTokenEncoder;
				private readonly IJwtTokenDecoder _jwtTokenDecoder;
				private readonly JwtAuthenticationSettings _jwtAuthenticationSettings;
				private readonly JwtTokenProviderOptions _jwtTokenProviderOptions;
		}
}
