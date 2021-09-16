/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Common.Utilities.AspNetCore.Response.Extensions
{
		// TODO: Move these to Common.Utilities.Extensions

		public static class AspNetCoreResponseExtensions
		{
				public static IActionResult ToResponse(this object obj)
				{
						var response = new ObjectResult(obj);

						return response;
				}

				public static IActionResult ToResponse(this object obj, HttpStatusCode httpStatusCode)
				{
						var response = new ObjectResult(obj)
						{
								StatusCode = (int)httpStatusCode
						};

						return response;
				}

				public static T GetInstance<T>(this IConfiguration configuration)
				{
						var instance = configuration.GetSection(typeof(T).Name).Get<T>();

						return instance;
				}
		}
}
