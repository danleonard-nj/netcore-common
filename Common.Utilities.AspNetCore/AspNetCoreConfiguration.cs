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

using Common.Utilities.Configuration.Managed;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.DependencyInjection.Registration;
using Common.Utilities.Jwt;
using Common.Utilities.Jwt.Configuration;
using Common.Utilities.Jwt.Dependencies.Exports;
using Common.Utilities.Middleware.ExceptionMiddleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Utilities.AspNetCore
{
		public static class AspNetCoreConfiguration
		{
				public static void ConfigureAspNetCoreServices<TDependencyExports>(this IServiceCollection serviceDescriptors,
						IWebHostEnvironment hostEnvironment, AspNetCoreConfigurationOptions aspNetCoreConfigurationOptions = default,
						JwtTokenProviderOptions jwtOptions = default)
						where TDependencyExports : IDependencyExport
				{
						var _aspNetCoreConfigurationOptions = aspNetCoreConfigurationOptions ?? new AspNetCoreConfigurationOptions();
						var _jwtOptions = jwtOptions ?? new JwtTokenProviderOptions();

						var managedConfiguration = new ManagedConfiguration(hostEnvironment,
								_aspNetCoreConfigurationOptions.InjectAzureKeyVaultSecrets);

						// Add controllers, exception handling

						serviceDescriptors.AddControllers(options =>
						{
								options.RegisterExceptionHandler(_aspNetCoreConfigurationOptions);
						});

						serviceDescriptors.RegisterDependencies<TDependencyExports>(managedConfiguration, _jwtOptions);

						// Swagger dependencies

						serviceDescriptors.AddSwaggerGen();
				}

				public static void RegisterExceptionHandler(this MvcOptions mvcOptions, AspNetCoreConfigurationOptions netCoreOptions)
				{
						if (netCoreOptions.ExceptionHandler != null)
						{
								mvcOptions.Filters.Add(netCoreOptions.ExceptionHandler);
						}
						else
						{
								mvcOptions.Filters.Add<CommonExceptionHandler>();
						}
				}

				public static void RegisterDependencies<T>(this IServiceCollection serviceDescriptors,
						IManagedConfiguration managedConfiguration,
						JwtTokenProviderOptions jwtTokenProviderOptions) where T : IDependencyExport
				{
						var exportRegistration = new DependencyExportRegistration(serviceDescriptors, managedConfiguration);

						exportRegistration.RegisterJwtAuthentication(jwtTokenProviderOptions);

						exportRegistration.RegisterDependencies<T>();
				}

				public static void RegisterJwtAuthentication(this DependencyExportRegistration exportRegistration,
						JwtTokenProviderOptions jwtOptions)
				{
						exportRegistration.RegisterDependencies<JwtDependencyExports>();

						exportRegistration.RegisterDependency<IJwtTokenProvider, JwtTokenProvider>(serviceProvider =>
						{
								var options = serviceProvider.ConfigureJwtTokenProviderOptions(jwtOptions);
								var provider = serviceProvider.ConfigureJwtTokenProvider(jwtOptions);

								return provider;
						});
				}	
		}
}
