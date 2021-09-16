/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System;

namespace Common.Utilities.DependencyInjection.Exceptions
{
		public class DependencyInjectionException : Exception
		{
				public Type ServiceType { get; set; }

				public DependencyInjectionException(string message, Type serviceType)
						: base(message)
				{
						ServiceType = serviceType;
				}
		}
}
