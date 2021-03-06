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


using Common.Utilities.Extensions.Base;
using System.Net;

namespace Common.Utilities.Exceptions.UserManagement.Base
{
		public class UserManagementException<TCallingClass> : CommonException
		{
				public UserManagementException(string callingMember, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
						: base(callingMember, message, typeof(TCallingClass))
				{
						StatusCode = statusCode;
				}
		}
}
