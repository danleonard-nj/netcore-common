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

using Common.Utilities.DependencyInjection.Helpers;
using System;

namespace Common.Utilities.DependencyInjection.Exports.Types
{
		public interface IServiceExport
		{
				RegistrationType RegistrationType { get; }
				Type ImplementationType { get; }
				Type ServiceType { get; }
		}

		public class ServiceExport : IServiceExport
		{
				public Type ServiceType { get => _serviceType; }
				public Type ImplementationType { get => _implementationType; }
				public RegistrationType RegistrationType { get => _registrationType; }


				public ServiceExport(Type serviceType, RegistrationType registrationType = RegistrationType.Scoped)
				{
						_serviceType = serviceType.IsInterface ? serviceType : throw new ArgumentException("Invalid type", nameof(serviceType));

						_registrationType = registrationType;
						_implementationType = TypeHelper.GetImplementationType(serviceType);
				}

				private readonly Type _serviceType;
				private readonly Type _implementationType;

				private readonly RegistrationType _registrationType;
		}

		public class ServiceExport<TService> : IServiceExport
		{
				public Type ServiceType { get => _serviceType; }
				public Type ImplementationType { get => _implementationType; }
				public RegistrationType RegistrationType { get => _registrationType; }

				public ServiceExport(RegistrationType registrationType = RegistrationType.Scoped, Type implementationType = null)
				{
						_serviceType = typeof(TService);
						_implementationType = implementationType ?? TypeHelper.GetImplementationType(typeof(TService));

						_registrationType = registrationType;

				}

				private readonly Type _implementationType;
				private readonly Type _serviceType;

				private readonly RegistrationType _registrationType;
		}

		public class ServiceExport<TService, TImplementation> : IServiceExport
		{
				public Type ServiceType { get => _serviceType; }
				public Type ImplementationType { get => _implementationType; }
				public RegistrationType RegistrationType { get => _registrationType; }

				public ServiceExport(RegistrationType registrationType = RegistrationType.Scoped)
				{
						_serviceType = typeof(TService);
						_implementationType = typeof(TImplementation);

						_registrationType = registrationType;

				}

				private readonly Type _implementationType;
				private readonly Type _serviceType;

				private readonly RegistrationType _registrationType;
		}
}
