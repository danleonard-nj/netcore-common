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


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Common.Utilities.Configuration
{
		public interface IManagedConfiguration
		{
				IConfiguration GetConfiguration(IHostEnvironment hostingEnvironment);
		}

		public class ManagedConfiguration : IManagedConfiguration
		{
				public IConfiguration GetConfiguration(IHostEnvironment hostingEnvironment)
				{
						var builder = new ConfigurationBuilder();

						if (hostingEnvironment.IsDevelopment())
						{
								builder.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json");
						}

						else
						{
								builder.AddJsonFile($"appsettings.json");
						}

						return builder.Build();
				}
		}
}
