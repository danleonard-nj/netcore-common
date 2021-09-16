/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Configuration.Binding;
using Common.Utilities.DependencyInjection.Exports.Types;
using Common.Utilities.DependencyInjection.Exports.Types.Abstractions;
using Common.Utilities.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Utilities.DependencyInjection.Registration
{
		public interface IDependencyExportRegistration
		{
				void RegisterDependencies<T>() where T : IDependencyExport;
				void RegisterDependency(IServiceExport serviceExport);
				void RegisterDependency<TServiceType, TImplementationType>(Func<ServiceProvider, TImplementationType> implementationFactory);
				object ResolveDependency(Type serviceType);
				T ResolveDependency<T>();
		}

		public class DependencyExportRegistration : IDependencyExportRegistration
		{
				public DependencyExportRegistration(IServiceCollection serviceDescriptors,
						IConsolidatedConfigurationProvider consolidatedConfigurationProvider)
				{
						_serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));
						_consolidatedConfigurationProvider = consolidatedConfigurationProvider ?? throw new ArgumentNullException(nameof(consolidatedConfigurationProvider));
				}

				public T ResolveDependency<T>()
				{
						var instance = _serviceDescriptors.ResolveDependency<T>();

						return instance;
				}

				public object ResolveDependency(Type serviceType)
				{
						var instance = _serviceDescriptors.ResolveDependency(serviceType);

						return instance;
				}

				public void RegisterDependencies<T>() where T : IDependencyExport
				{
						var exports = Activator.CreateInstance<T>();

						foreach (var settingsExport in exports.GetSettingsExports())
						{
								_serviceDescriptors.RegisterSettingsExport(settingsExport, _consolidatedConfigurationProvider);
						}

						foreach (var serviceExport in exports.GetServiceExports())
						{
								_serviceDescriptors.RegisterServiceExport(serviceExport);
						}
				}

				public void RegisterDependency<TServiceType, TImplementationType>(Func<ServiceProvider, TImplementationType> implementationFactory)
				{
						var serviceProvider = _serviceDescriptors.BuildServiceProvider();

						var instance = implementationFactory(serviceProvider);

						var serviceExport = new ServiceExport<TServiceType, TImplementationType>(instance);

						_serviceDescriptors.RegisterServiceExport(serviceExport);
				}

				public void RegisterDependency(IServiceExport serviceExport)
				{
						_serviceDescriptors.RegisterServiceExport(serviceExport);
				}

				private readonly IConsolidatedConfigurationProvider _consolidatedConfigurationProvider;
				private readonly IServiceCollection _serviceDescriptors;
		}
}
