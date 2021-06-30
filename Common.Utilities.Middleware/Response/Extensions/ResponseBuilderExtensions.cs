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
