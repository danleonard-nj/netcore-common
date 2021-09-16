/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.UserManagement.Abstractions;

namespace Common.Models.UserManagement
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
