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

using Common.Utilities.DependencyInjection.Exports;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities.DependencyInjection.Registration
{
		public class DependencyExportRegistration
		{
				public DependencyExportRegistration()
				{
						_exportFactory = new DependencyExportFactory();						
				}

				public void RegisterDependencies<T>(IServiceCollection serviceDescriptors, IConfiguration configuration)
						where T : IDependencyExport
				{
						var exports = _exportFactory.GetExports<T>();

						var settingsExports = exports.GetSettingsExports();

						RegisterSettingsExports(settingsExports, serviceDescriptors, configuration);

						var serviceExports = exports.GetServiceExports();

						RegisterServiceExports(serviceExports, serviceDescriptors);
				}
				
				private void RegisterServiceExports(IEnumerable<IServiceExport> serviceExports,
						IServiceCollection serviceDescriptors)
				{
						var exportList = serviceExports.ToList();

						exportList.ForEach(x => x.RegisterServiceExport(serviceDescriptors));
				}

				private void RegisterSettingsExports(IEnumerable<ISettingsExport> settingsExports,
						IServiceCollection serviceDescriptors,
						IConfiguration configuration)
				{
						var settingsList = settingsExports.ToList();

						settingsList.ForEach(x => x.RegisterSettingsExport(serviceDescriptors, configuration));
				}

				private readonly IDependencyExportFactory _exportFactory;
		}
}
