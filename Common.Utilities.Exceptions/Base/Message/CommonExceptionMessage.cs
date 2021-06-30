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


using System;

namespace Common.Utilities.Exceptions.Base.Message
{
		public static class CommonExceptionMessage
		{
				public static string Message(Type callingClass, string callingMember, string message)
				{
						var exceptionMessage = $"{callingClass.FullName}: {callingMember}: {message}";

						return exceptionMessage;
				}
		}
}
