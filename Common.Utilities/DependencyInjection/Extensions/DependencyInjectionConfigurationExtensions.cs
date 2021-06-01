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

using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Utilities.DependencyInjection.Extensions
{
		public static class DependencyInjectionConfigurationExtensions
		{
				public static void RegisterServiceExport(this IServiceExport serviceExport, IServiceCollection serviceDescriptors)
				{
						switch (serviceExport.RegistrationType)
						{
								case RegistrationType.Scoped:
										serviceDescriptors.AddScoped(serviceExport.ServiceType, serviceExport.ImplementationType ?? TypeHelper.GetImplementationType(serviceExport.ServiceType));
										break;

								case RegistrationType.Singleton:
										serviceDescriptors.AddSingleton(serviceExport.ServiceType, serviceExport.ImplementationType ?? TypeHelper.GetImplementationType(serviceExport.ServiceType));
										break;

								case RegistrationType.Transient:
										serviceDescriptors.AddTransient(serviceExport.ServiceType, serviceExport.ImplementationType ?? TypeHelper.GetImplementationType(serviceExport.ServiceType));
										break;

								default:
										break;
						}
				}

				public static void RegisterSettingsExport(this ISettingsExport settingsExport, IServiceCollection serviceDescriptors, IConfiguration configuration)
				{
						serviceDescriptors.AddSingleton(settingsExport.Type, TypeHelper.GetInstance(configuration, settingsExport.Type));
				}
		}
}
