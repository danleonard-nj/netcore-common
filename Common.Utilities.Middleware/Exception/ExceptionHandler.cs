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


using Common.Utilities.Middleware.Exceptions.Abstractions;
using Common.Utilities.Middleware.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.Exceptions
{
		public class ExceptionHandler : ExceptionHandlerMiddleware
		{
				public ExceptionHandler(RequestDelegate requestDelegate)
						: base(requestDelegate)
				{
				}

				public override async Task<ObjectResult> HandleException(Exception exception)
				{
						await Task.Yield();

						var exceptionModel = new ExceptionResponseModel
						{
								IsError = true,
								ErrorType = exception.GetType().Name,
								ErrorMessage = exception.Message
						};

						var response = new ObjectResult(exceptionModel)
						{
								StatusCode = Context.Response.StatusCode
						};

						return response;
				}
		}
}
	