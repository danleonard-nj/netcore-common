/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Configuration.Binding;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.DependencyInjection.Registration;
using Common.Utilities.Jwt;
using Common.Utilities.Jwt.Dependencies.Exports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Utilities.AspNetCore
{
		public static class AspNetCoreConfiguration
		{
				public static void RegisterDependencies<TDependencyExports>(this IServiceCollection serviceDescriptors,
						IConfiguration configuration, AspNetCoreConfigurationOptions aspNetCoreConfigurationOptions = default)
						where TDependencyExports : IDependencyExport
				{
						var _aspNetCoreConfigurationOptions = aspNetCoreConfigurationOptions ?? new AspNetCoreConfigurationOptions();

						// Swagger

						serviceDescriptors.AddSwaggerGen();

						// Configurations

						var consolidatedProvider = new ConsolidatedConfigurationProvider(
								_aspNetCoreConfigurationOptions.AzureKeyVaultUri,
								_aspNetCoreConfigurationOptions.Environment);

						// Exports

						var exportRegistration = new DependencyExportRegistration(
								serviceDescriptors, 
								consolidatedProvider);

						exportRegistration.RegisterDependencies<TDependencyExports>();
						exportRegistration.RegisterDependencies<JwtDependencyExports>();
				}

				public static void ConfigureJwtAuthentication(this IServiceCollection serviceDescriptors,
						JwtTokenProviderOptions jwtOptions)
				{
						// Register the JWT token options

						serviceDescriptors.AddSingleton(jwtOptions);
				}	
		}
}
