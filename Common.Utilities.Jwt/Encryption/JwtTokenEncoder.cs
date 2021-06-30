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


using JWT;
using JWT.Algorithms;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Jwt.Encryption
{
		public interface IJwtTokenEncoder
		{
				Task<string> GetEncodedToken<IJwtPayload>(IJwtPayload payload, byte[] encryptionKey);
		}

		public class JwtTokenEncoder : IJwtTokenEncoder
		{
				public JwtTokenEncoder(IJsonSerializer jsonSerializer,
						IBase64UrlEncoder urlEncoder,
						IJwtAlgorithm jwtAlgorithm)
				{
						if (jsonSerializer == null)
						{
								throw new ArgumentNullException(nameof(jsonSerializer));
						}

						if (urlEncoder == null)
						{
								throw new ArgumentNullException(nameof(urlEncoder));
						}

						if (jwtAlgorithm == null)
						{
								throw new ArgumentNullException(nameof(jwtAlgorithm));
						}

						_encoder = new JwtEncoder(jwtAlgorithm,
								jsonSerializer,
								urlEncoder);
				}

				public async Task<string> GetEncodedToken<IJwtPayload>(IJwtPayload payload, byte[] encryptionKey)
				{
						await Task.Yield();

						var token = _encoder.Encode(payload, encryptionKey);

						return token;
				}

				private readonly IJwtEncoder _encoder;
		}
}
