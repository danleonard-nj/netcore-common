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

using Common.Utilities.Authentication.Jwt;
using Common.Utilities.Authentication.Settings;
using Common.Utilities.DependencyInjection;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Collections.Generic;

namespace Common.Utilities.Authentication.DependencyInjection.Exports
{
		public class AuthenticationDependencyExports : IDependencyExport
		{
				public IEnumerable<IServiceExport> GetServiceExports()
				{
						var services = new List<IServiceExport>
						{
								// Middleware dependencies (must be singleton)
								
								new ServiceExport<IJsonSerializer, JsonNetSerializer>(RegistrationType.Singleton),
								new ServiceExport<IJwtAlgorithm, HMACSHA256Algorithm>(RegistrationType.Singleton),
								new ServiceExport<IBase64UrlEncoder, JwtBase64UrlEncoder>(RegistrationType.Singleton),
								new ServiceExport<IDateTimeProvider, UtcDateTimeProvider>(RegistrationType.Singleton),
								new ServiceExport<IJwtDependencyProvider, JwtDependencyProvider>(RegistrationType.Singleton),
								new ServiceExport<IJwtTokenUtility, JwtTokenUtility>(),

						};

						return services; 
				}

				public IEnumerable<ISettingsExport> GetSettingsExports()
				{
						var settings = new List<ISettingsExport>
						{
								new SettingsExport<AuthenticationSettings>()
						};

						return settings;
				}
		}
}
