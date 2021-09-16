/* Copyright (C) 2021 Dan Leonard
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
