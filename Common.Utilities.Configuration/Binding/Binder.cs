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


using Common.Utilities.Configuration.AzureKeyVault.Extensions;
using Common.Utilities.Configuration.Managed;
using Microsoft.Extensions.Configuration;
using System;

namespace Common.Utilities.Configuration.Binding
{
		public interface IBinder
		{
				object BindConfiguration(Type type);
				T BindConfiguration<T>();
		}

		public class Binder : IBinder
		{
				public Binder(IManagedConfiguration configuration, bool addAzureKeyVault = false)
				{
						_managedConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
				}

				public object BindConfiguration(Type type)
				{
						var instance = Activator.CreateInstance(type);

						_managedConfiguration.BuildDefaultConfiguration();
						var configuration = _managedConfiguration.GetConfiguration();

						configuration.GetSection(type.Name).Bind(instance);

						if (_managedConfiguration.IsKeyVault)
						{
								if (instance.KeyVaultAttributesDefined())
								{
										InjectKeyVaultVariables(instance, _managedConfiguration);
								}
						}

						return instance;
				}

				public T BindConfiguration<T>()
				{
						var type = typeof(T);

						var instance = BindConfiguration(type);

						return (T)instance;
				}

				private void InjectKeyVaultVariables(object instance, IManagedConfiguration managedConfiguration)
				{
						managedConfiguration.BuildAzureKeyVaultConfiguration();

						var keyVaultConfiguration = managedConfiguration.GetConfiguration();

						instance.InjectKeyVaultValues(keyVaultConfiguration);
				}

				private readonly IManagedConfiguration _managedConfiguration;
		}
}
