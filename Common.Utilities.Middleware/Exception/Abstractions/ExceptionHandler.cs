/* Copyright (C) 2012, 2013 Dan Leonard
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


using Common.Utilities.Extensions.Base;
using Common.Utilities.Middleware.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.ExceptionHandler
{
		public abstract class ExceptionHandlerMiddleware : ICustomMiddleware
		{
				protected HttpContext Context { get => _context; }

				protected ExceptionHandlerMiddleware(RequestDelegate next)
				{
						_next = next;
				}

				public async Task Invoke(HttpContext context)
				{
						_context = context;

						try
						{
								await _next(context);
						}
						catch (CommonException exception)
						{
								var response = await HandleException(exception);

								await WriteResponse(response);
						}
				}

				public abstract Task<ObjectResult> HandleException(Exception exception);

				protected async Task WriteResponse(ObjectResult result)
				{
						var responseJson = JsonConvert.SerializeObject(result);
						Context.Response.ContentType = "application/json";
						Context.Response.StatusCode = (int)result.StatusCode;

						await Context.Response.WriteAsync(responseJson);
				}

				private HttpContext _context;
				private readonly RequestDelegate _next;
		}
}
