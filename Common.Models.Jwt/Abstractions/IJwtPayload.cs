/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Models.Jwt.Abstractions
{
		public interface IJwtPayload
		{
				string EmailAddress { get; set; }
				long ExpiresOn { get; set; }
				string Username { get; set; }
		}
}
