/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.Jwt.Settings;
using Common.Utilities.Cryptography;
using Common.Utilities.DependencyInjection;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.Jwt.Dependencies.Providers;
using Common.Utilities.Jwt.Encryption;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Common.Utilities.Jwt.Dependencies.Exports
{
		public class JwtDependencyExports : IDependencyExport
		{
				public IEnumerable<IServiceExport> GetServiceExports()
				{
						var serviceExports = new List<IServiceExport>
						{
								new ServiceExport<IJsonSerializer, JsonNetSerializer>(RegistrationType.Singleton),
								new ServiceExport<IJwtAlgorithm, HMACSHA256Algorithm>(RegistrationType.Singleton),
								new ServiceExport<IBase64UrlEncoder, JwtBase64UrlEncoder>(RegistrationType.Singleton),
								new ServiceExport<IDateTimeProvider, UtcDateTimeProvider>(RegistrationType.Singleton),
								new ServiceExport<ISecurityTokenValidator, JwtSecurityTokenHandler>(RegistrationType.Singleton),
								new ServiceExport<ICryptoUtility, CryptoUtility>(RegistrationType.Singleton),
								new ServiceExport<IJwtTokenEncoder, JwtTokenEncoder>(RegistrationType.Singleton),
								new ServiceExport<IJwtTokenDecoder, JwtTokenDecoder>(RegistrationType.Singleton),
								new ServiceExport<IJwtDependencyProvider, JwtDependencyProvider>(RegistrationType.Singleton),
								new ServiceExport<IJwtTokenProvider, JwtTokenProvider>(RegistrationType.Singleton)
						};

						return serviceExports;
				}

				public IEnumerable<ISettingsExport> GetSettingsExports()
				{
						var settingsExports = new List<ISettingsExport>
						{
								new SettingsExport<JwtAuthenticationSettings>()
						};

						return settingsExports;
				}
		}
}
