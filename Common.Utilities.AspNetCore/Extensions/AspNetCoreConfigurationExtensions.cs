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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Utilities.AspNetCore.Extensions
{
		public static class AspNetCoreConfigurationExtensions
		{
				[Obsolete]
				// Using IHostingEnvironemtn due to issues with IWebHostEnvironment in extenal classes
				public static void ConfigureAspNetCoreServices<TDependencyExports>(this IServiceCollection serviceDescriptors,
						IHostingEnvironment hostingEnvironment, ConfigureAspNetCoreServicesOptions configureAspNetCoreServicesOptions = default) 
						where TDependencyExports : IDependencyExport
				{
						var options = configureAspNetCoreServicesOptions ?? new ConfigureAspNetCoreServicesOptions();

						var exportRegistration = new DependencyExportRegistration(hostingEnvironment, options.InjectKeyVaultSecrets);

						// Internal dependencies

						exportRegistration.RegisterDependencies<AuthenticationDependencyExports>(serviceDescriptors);
						exportRegistration.RegisterDependencies<UserManagementDependencyExports>(serviceDescriptors);

						// App defined dependencies

						exportRegistration.RegisterDependencies<TDependencyExports>(serviceDescriptors);

						// Swagger dependencies

						serviceDescriptors.AddSwaggerGen();
				}
		}
}
