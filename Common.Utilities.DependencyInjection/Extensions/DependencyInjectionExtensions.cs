/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Configuration.Binding;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Utilities.DependencyInjection.Extensions
{
		public static class DependencyInjectionExtensions
		{
				#region Service Export Registration

				private static void RegisterServiceInstance(this IServiceCollection serviceDescriptors, IServiceExport serviceExport)
				{
						switch (serviceExport.RegistrationType)
						{
								case RegistrationType.Scoped:
										serviceDescriptors.AddScoped(serviceExport.ServiceType, x => serviceExport.ImplementationInstance);
										break;

								case RegistrationType.Transient:
										serviceDescriptors.AddScoped(serviceExport.ServiceType, x => serviceExport.ImplementationInstance);
										break;

								case RegistrationType.Singleton:
										serviceDescriptors.AddSingleton(serviceExport.ServiceType, x => serviceExport.ImplementationInstance);
										break;
						}
				}

				private static void RegisterService(this IServiceCollection serviceDescriptors, IServiceExport serviceExport)
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

				public static void RegisterServiceExport(this IServiceCollection serviceDescriptors, IServiceExport serviceExport)
				{
						if (serviceExport.IsImplementationInstance)
						{
								serviceDescriptors.RegisterServiceInstance(serviceExport);
						}

						else
						{
								serviceDescriptors.RegisterService(serviceExport);
						}
				}

				#endregion

				#region Settings Export Registration

				public static void RegisterSettingsExport(this IServiceCollection serviceDescriptors, ISettingsExport settingsExport, IConsolidatedConfigurationProvider provider)
				{
						var instance = provider.BindInstance(settingsExport.Type);

						serviceDescriptors.AddSingleton(settingsExport.Type, instance);
				}

				#endregion

				#region Resolve Dependency

				public static T ResolveDependency<T>(this IServiceCollection serviceDescriptors)
				{
						var provider = serviceDescriptors.BuildServiceProvider();

						var instance = provider.GetService<T>();
;
						return instance;
				}

				public static object ResolveDependency(this IServiceCollection serviceDescriptors, Type serviceType)
				{
						var provider = serviceDescriptors.BuildServiceProvider();

						var instance = provider.GetService(serviceType);

						return instance;
				}

				#endregion
		}
}
