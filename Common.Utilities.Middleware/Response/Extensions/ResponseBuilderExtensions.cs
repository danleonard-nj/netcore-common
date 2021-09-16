/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System;
using System.IO;

namespace Common.Utilities.Middleware.Response.Extensions
{
		public static class ResponseBuilderExtensions
		{
				public static void Rewind(this Stream stream)
				{
						if (stream.Position != 0)
						{
								stream.Seek(0, SeekOrigin.Begin);
						}
				}

				public static TResult Rewind<TStream, TResult>(this TStream stream, Func<TStream, TResult> func) where TStream : Stream
				{
						stream.Rewind();

						var result = func(stream);

						stream.Rewind();

						return result;
				}

				public static void Rewind<TStream>(this TStream stream, Action<TStream> func) where TStream : Stream
				{
						stream.Rewind();

						func(stream);

						stream.Rewind();
				}
		}
}
