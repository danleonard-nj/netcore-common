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

using Common.Models.AspNetCore.Options;
using Common.Utilities.Authentication.DependencyInjection.Exports;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.DependencyInjection.Registration;
using Common.Utilities.UserManagement.DependencyInjection.Exports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Utilities.AspNetCore.Extensions
{
		public static class AspNetCoreConfigurationExtensions
		{
				public static void ConfigureAspNetCoreServices<TDependencyExports>(this IServiceCollection serviceDescriptors,
						IWebHostEnvironment webHostEnvironment, ConfigureAspNetCoreServicesOptions configureAspNetCoreServicesOptions = default) 
						where TDependencyExports : IDependencyExport
				{
						var options = configureAspNetCoreServicesOptions ?? new ConfigureAspNetCoreServicesOptions();

						var configuration = GetConfiguration(webHostEnvironment);

						var exportRegistration = new DependencyExportRegistration(configuration, options.InjectKeyVaultSecrets);

						// Internal dependencies

						exportRegistration.RegisterDependencies<AuthenticationDependencyExports>(serviceDescriptors, configuration);
						exportRegistration.RegisterDependencies<UserManagementDependencyExports>(serviceDescriptors, configuration);

						// App defined dependencies

						exportRegistration.RegisterDependencies<TDependencyExports>(serviceDescriptors, configuration);

						// Swagger dependencies

						serviceDescriptors.AddSwaggerGen();
				}

				public static IConfiguration GetConfiguration(IWebHostEnvironment hostEnvironment)
				{
						var configurationBuilder = new ConfigurationBuilder();

						if (hostEnvironment.IsDevelopment())
						{
								configurationBuilder.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true);
						}
						else
						{
								configurationBuilder.AddJsonFile("appsettings.json", optional: true);
						}

						var configuration = configurationBuilder.Build();

						return configuration;
				}
		}
}
