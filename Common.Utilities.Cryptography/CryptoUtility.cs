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


using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utilities.Cryptography
{
		public interface ICryptoUtility
		{
				string CreateHash(object source, bool serialize = false);
				string CreateHash(object[] sources, bool serialize = false);
				SaltedHash GenerateSaltedHash(string password);
				string GenerateSaltedHash(string password, byte[] salt);
				string GenerateSaltedHash(string password, string salt);
				bool VerifySaltedHash(string provided, string saltedHash, string salt);
		}

		public class CryptoUtility : ICryptoUtility
		{
				public CryptoUtility()
				{
						_rngCryptoProvider = new RNGCryptoServiceProvider();
				}

				public string CreateHash(object source, bool serialize = false)
				{
						var sourceString = serialize ? JsonConvert.SerializeObject(source) : source;
						var bytes = Encoding.UTF8.GetBytes(sourceString.ToString());
						var hash = Convert.ToBase64String(bytes);

						return hash;
				}

				public string CreateHash(object[] sources, bool serialize = false)
				{
						var strings = sources
								.Select(x => x?.ToString() ?? string.Empty)
								.Select(x => serialize ? JsonConvert.SerializeObject(x) : x);

						var joined = string.Join(string.Empty, strings);

						var bytes = Encoding.UTF8.GetBytes(joined);
						var hash = Convert.ToBase64String(bytes);

						return hash;
				}

				public SaltedHash GenerateSaltedHash(string password)
				{
						var salt = GetSalt();

						var hash = GenerateSaltedHash(password, salt);

						var saltedHash = new SaltedHash(hash, salt);

						return saltedHash;
				}

				public string GenerateSaltedHash(string password, byte[] salt)
				{
						var hash = KeyDerivation.Pbkdf2(
								password: password,
								salt: salt,
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 10000,
								numBytesRequested: 256 / 8);

						var hashString = Convert.ToBase64String(hash);

						return hashString;
				}

				public string GenerateSaltedHash(string password, string salt)
				{
						var saltBytes = GetSalt(salt);

						var hash = GenerateSaltedHash(password, saltBytes);

						return hash;
				}

				public bool VerifySaltedHash(string provided, string saltedHash, string salt)
				{
						var hash = GenerateSaltedHash(provided, salt);

						if (saltedHash == hash)
						{
								return true;
						}

						return false;
				}

				private byte[] GetSalt()
				{
						var salt = new byte[16];

						_rngCryptoProvider.GetNonZeroBytes(salt);

						return salt;
				}

				private byte[] GetSalt(string salt)
				{
						var saltBytes = Convert.FromBase64String(salt);

						return saltBytes;
				}

				private readonly RNGCryptoServiceProvider _rngCryptoProvider;
		}
}
