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
				public Binder(IConfiguration configuration, bool addAzureKeyVault = false)
				{
						_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
						_addAzureKeyVault = addAzureKeyVault;
				}

				public object BindConfiguration(Type type)
				{
						var instance = Activator.CreateInstance(type);

						_configuration.GetSection(type.Name).Bind(instance);

						if (_addAzureKeyVault)
						{
								if (instance.KeyVaultValuesDefined())
								{
										InjectKeyVaultVariables(instance, _configuration);
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

				private void InjectKeyVaultVariables(object instance, IConfiguration configuration)
				{
						var keyVaultConfiguration = configuration.GetAzureKeyVaultConfiguration();

						instance.InjectKeyVaultVariables(keyVaultConfiguration);
				}

				private readonly IConfiguration _configuration;
				private bool _addAzureKeyVault;
		}
}
