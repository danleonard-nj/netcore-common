/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Common.Models.Jwt;
using JWT;
using JWT.Algorithms;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.Jwt.Encryption
{
		public interface IJwtTokenDecoder
		{
				Task<JwtPayload> GetDecodedToken(string token, byte[] encryptionKey);
				Task<T> GetDecodedToken<T>(string token, byte[] encryptionKey);
		}

		public class JwtTokenDecoder : IJwtTokenDecoder
		{
				public JwtTokenDecoder(IJsonSerializer jsonSerializer,
						IBase64UrlEncoder base64UrlEncoder,
						IJwtAlgorithm jwtAlgorithm,
						IDateTimeProvider dateTimeProvider)
				{

						if (jsonSerializer == null)
						{
								throw new ArgumentNullException(nameof(jsonSerializer));
						}

						if (base64UrlEncoder == null)
						{
								throw new ArgumentNullException(nameof(base64UrlEncoder));
						}

						if (jwtAlgorithm == null)
						{
								throw new ArgumentNullException(nameof(jwtAlgorithm));
						}

						var jwtValidator = new JwtValidator(jsonSerializer, dateTimeProvider);

						_jwtDecoder = new JwtDecoder(jsonSerializer, jwtValidator, base64UrlEncoder, jwtAlgorithm);
				}

				public async Task<JwtPayload> GetDecodedToken(string token, byte[] encryptionKey)
				{
						await Task.Yield();

						var decoded = _jwtDecoder.DecodeToObject<JwtPayload>(token, encryptionKey, true);

						return decoded;
				}

				public async Task<T> GetDecodedToken<T>(string token, byte[] encryptionKey)
				{
						await Task.Yield();

						var decoded = _jwtDecoder.DecodeToObject<T>(token, encryptionKey, true);

						return decoded;
				}

				private readonly IJwtDecoder _jwtDecoder;
		}
}
