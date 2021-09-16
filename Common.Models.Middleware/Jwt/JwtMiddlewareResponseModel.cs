/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.AspNetCore.Response.Abstractions;

namespace Common.Models.Middleware.Jwt
{
		public class JwtMiddlewareResponseModel : IResponseModel
		{
				public object Content { get; set; }
				public object Exception { get; set; }
				public bool IsError { get; set; }
		}
}
