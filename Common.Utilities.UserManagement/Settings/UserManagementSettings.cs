/* Copyright (C) 2012, 2013 Dan Leonard
 * This file is part of DMP Management App.
 * 
 * DMP Management App is free software: you can redistribute it and/or 
 * modify it under the terms of the GNU General Public License as published 
 * by the Free Software Foundation, either version 3 of the License, or (at
 * your option) any later version.
 * 
 * DMP Management App is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for 
 * more details.
 */

using Common.Utilities.Configuration.AzureKeyVault.Attributes;

namespace Common.Utilities.UserManagement.Settings
{
		public class UserManagementSettings
		{
				[AzureKeyVaultSecret]
				public string SqlConnectionString { get; set; }
		}
}
