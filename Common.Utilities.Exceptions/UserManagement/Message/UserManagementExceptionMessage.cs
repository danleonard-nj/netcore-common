﻿/* Copyright (C) 2012, 2013 Dan Leonard
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


namespace Common.Utilities.Exceptions.UserManagement.Message
{
		public static class UserManagementExceptionMessage
		{
				public static string UsernameExists(string username)
				{
						return $"{username} already exists.";
				}

				public static string InvalidPassword()
				{
						return $"The supplied password is incorrect.";
				}
		}
}
