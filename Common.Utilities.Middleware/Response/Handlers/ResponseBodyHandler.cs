/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
