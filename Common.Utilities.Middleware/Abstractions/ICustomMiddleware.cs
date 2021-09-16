/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.Abstractions
{
		public interface ICustomMiddleware
		{
				Task Invoke(HttpContext context);
		}
}
