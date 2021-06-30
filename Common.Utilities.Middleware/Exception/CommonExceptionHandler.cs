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
using Common.Utilities.Middleware.ExceptionMiddleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.ExceptionHandler
{
		public class CommonExceptionHandler : ExceptionHandlerMiddleware
		{
				public CommonExceptionHandler(RequestDelegate requestDelegate)
						: base(requestDelegate)
				{
				}

				public override async Task<ObjectResult> HandleException(Exception exception)
				{
						await Task.Yield();

						var statusCode = HttpStatusCode.InternalServerError;

						if (exception is CommonException commonException)
						{
								statusCode = commonException.StatusCode;
						}
						var exceptionModel = new ExceptionResponseModel
						{
								IsError = true,
								ErrorType = exception.GetType().Name,
								ErrorMessage = exception.Message
						};

						var response = new ObjectResult(exceptionModel)
						{
								StatusCode = (int)statusCode
						};

						return response;
				}
		}
}
	