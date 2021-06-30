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


using Common.Utilities.Middleware.Response.Extensions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Common.Utilities.Middleware.Response.Handlers
{
		public class ResponseBodyHandler
		{
				public ResponseBodyHandler()
				{
						_memoryStream = new MemoryStream();
				}

				public void SetContext(HttpContext context)
				{
						_context = context;
				}

				public async Task<string> GetContent()
				{
						var originalResponseBody = _context.Response.Body;

						var memorystream = new MemoryStream();

						_context.Response.Body = memorystream;

						var responseBody = await memorystream.Rewind(async stream =>
						{
								return await new StreamReader(stream).ReadToEndAsync();
						});

						Debug.WriteLine("Response:");
						Debug.WriteLine(responseBody);

						await memorystream.CopyToAsync(originalResponseBody);

						return responseBody;
				}

				public async Task SetContent(string content)
				{
						var contentBuffer = new MemoryStream();

						contentBuffer.Rewind(stream =>
						{
								var writer = new StreamWriter(contentBuffer);

								writer.Write(content);
								writer.Flush();
						});

						await contentBuffer.CopyToAsync(_originalResponseContent);
				}

				private Stream _originalResponseContent;
				private MemoryStream _memoryStream;
				private HttpContext _context;
		}
}
