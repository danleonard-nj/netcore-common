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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Utilities.Logging.Configuration.Extensions
{
		public static class LoggingConfigurationExtensions
		{
				public static void AddDefaultAzureLogging(this IWebHostBuilder webHostBuilder)
				{
						webHostBuilder.ConfigureLogging(logging =>
						{
								logging.ClearProviders();
								logging.AddConsole();
								logging.AddAzureWebAppDiagnostics();
						});
				}
		}
}
