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

using Common.Utilities.Cryptography;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Exports.Types.Interfaces;
using Common.Utilities.UserManagement.Components;
using Common.Utilities.UserManagement.Data;
using Common.Utilities.UserManagement.Settings;
using System.Collections.Generic;

namespace Common.Utilities.UserManagement.DependencyInjection.Exports
{
		public class UserManagementDependencyExports : IDependencyExport
		{
				public IEnumerable<IServiceExport> GetServiceExports()
				{
						var services = new List<IServiceExport>
						{
								new ServiceExport<ICryptoUtility, CryptoUtility>(),
								new ServiceExport<IUserRepository, UserRepository>(),
								new ServiceExport<IUserManagementComponent, UserManagementComponent>()
						};

						return services;
				}

				public IEnumerable<ISettingsExport> GetSettingsExports()
				{
						var settings = new List<ISettingsExport>
						{
								new SettingsExport<UserManagementSettings>()
						};

						return settings;
				}
		}
}
