/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Utilities.Middleware.Response.Abstractions
{
		public interface IResponseBuilder
		{
				object CreateResponse(string body);
		}
}
