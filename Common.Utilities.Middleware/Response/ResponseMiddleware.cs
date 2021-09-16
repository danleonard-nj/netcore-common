/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Utilities.Middleware.Response.Abstractions;
using Common.Utilities.Middleware.Response.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.Response
{

		public class ResponseMiddleware<T> where T : IResponseBuilder
		{
				public ResponseMiddleware(RequestDelegate requestDelegate)
				{
						_next = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
						_builder = GetResponseBuilder();
				}

				private IResponseBuilder GetResponseBuilder()
				{
						var instance = Activator.CreateInstance<T>();

						return instance;
				}

				public async Task Invoke(HttpContext context)
				{
						var initialResponseBody = context.Response.Body;

						var buffer = new MemoryStream();

						context.Response.Body = buffer;

						await _next(context);

						var responseBody = await buffer.Rewind(async stream =>
						{
								var content = await new StreamReader(stream).ReadToEndAsync();

								return content;
						});

						var modifiedStream = new MemoryStream();

						await modifiedStream.Rewind(async stream =>
						{
								var writer = new StreamWriter(stream);

								var modifiedContent = _builder.CreateResponse(responseBody);

								await writer.WriteAsync(JsonConvert.SerializeObject(modifiedContent));

								await writer.FlushAsync();
						});

						await modifiedStream.CopyToAsync(initialResponseBody);
				}


				private readonly IResponseBuilder _builder;
				private readonly RequestDelegate _next;
		}
}
