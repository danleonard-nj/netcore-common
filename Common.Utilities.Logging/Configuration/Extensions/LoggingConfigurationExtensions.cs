/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
