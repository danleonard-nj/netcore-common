/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.Jwt.Abstractions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Common.Models.Jwt
{

		public class JwtPayload : IJwtPayload
		{
				[JsonProperty(ClaimTypes.NameIdentifier)]
				public int UserId { get; set; }

				[JsonProperty(ClaimTypes.Name)]
				public string Username { get; set; }

				[JsonProperty(ClaimTypes.Email, NullValueHandling = NullValueHandling.Ignore)]
				public string EmailAddress { get; set; }

				[JsonProperty("exp")]
				public long ExpiresOn { get; set; }
		}
}
