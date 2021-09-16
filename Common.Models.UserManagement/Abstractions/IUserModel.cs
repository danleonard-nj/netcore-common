/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Models.UserManagement.Abstractions
{
		public interface IUserModel
		{
				int UserId { get; set; }
				string Username { get; set; }
				string Email { get; set; }
				string Password { get; set; }
				string Salt { get; set; }
				string Role { get; set; }
		}
}
