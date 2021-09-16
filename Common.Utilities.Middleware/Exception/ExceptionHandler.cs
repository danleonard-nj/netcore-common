/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
	