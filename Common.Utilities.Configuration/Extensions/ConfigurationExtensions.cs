using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities.Configuration.Extensions
{
		public static class ConfigurationExtensions
		{
				public static T GetInstance<T>(this IConfiguration configuration)
				{
						var instance = configuration.GetSection(typeof(T).Name).Get<T>();

						return instance;
				}
		}
}
