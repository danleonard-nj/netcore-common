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

using Common.Utilities.UserManagement.Models.Interfaces;

namespace Common.Utilities.UserManagement.Models
{

		public class UserModel : IUserModel
		{
				public int UserId { get; set; }
				public string Username { get; set; }
				public string Email { get; set; }
				public string Role { get; set; }
				public string Password { get; set; }
				public string Salt { get; set; }
		}
}
