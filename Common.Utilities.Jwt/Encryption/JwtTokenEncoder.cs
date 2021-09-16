/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
