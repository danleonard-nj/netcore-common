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


using Common.Models.Jwt;
using Common.Models.UserManagement.Abstractions;
using Common.Utilities.Cryptography;
using Common.Utilities.Jwt;
using System;
using System.Threading.Tasks;

namespace Common.Utilities.UserManagement.Abstractions
{
		public interface IUserManagement
		{
				Task<string> AuthenticateUser(IUserModel user);
				Task<bool> CreateUser(IUserModel user);
				Task<bool> DeleteUser(int userId);
				Task<PasswordHash> GenerateHashedPassword(string password);
				Task<string> GetIdentityToken(IUserModel user);
				Task<IUserModel> GetUser(string username);
				Task<IUserModel> UpdateUser(IUserModel user);
				Task<bool> VerifyHashedPassword(string source, string compare, string salt);
		}

		public abstract class UserManagementBase : IUserManagement 
		{
				protected UserManagementBase(IJwtTokenProvider jwtTokenProvider,
						ICryptoUtility cryptoUtility)
				{
						_jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
						_cryptoUtility = cryptoUtility ?? throw new ArgumentNullException(nameof(cryptoUtility));
				}

				public abstract Task<string> AuthenticateUser(IUserModel user);

				public abstract Task<bool> CreateUser(IUserModel user);

				public abstract Task<bool> DeleteUser(int userId);

				public abstract Task<IUserModel> UpdateUser(IUserModel user);

				public abstract Task<IUserModel> GetUser(string username);

				public virtual async Task<PasswordHash> GenerateHashedPassword(string password)
				{
						await Task.Yield();

						var saltedHash = _cryptoUtility.GenerateSaltedHash(password);

						var passwordHash = new PasswordHash
						{
								Password = saltedHash.Hash,
								Salt = Convert.ToBase64String(saltedHash.Salt)
						};

						return passwordHash;
				}

				public virtual async Task<bool> VerifyHashedPassword(string source, string compare, string salt)
				{
						await Task.Yield();

						var comparisonHash = _cryptoUtility.GenerateSaltedHash(compare, salt);

						if (comparisonHash != source)
						{
								return false;
						}

						return true;
				}

				public virtual async Task<string> GetIdentityToken(IUserModel user)
				{
						await Task.Yield();

						var payload = new JwtPayload
						{
								EmailAddress = user.Email,
								UserId = user.UserId,
								Username = user.Username,
								ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenProvider.TokenLifetime).ToUnixTimeSeconds()
						};

						var token = await _jwtTokenProvider.GetToken(payload);

						return token;
				}

				private readonly IJwtTokenProvider _jwtTokenProvider;
				private readonly ICryptoUtility _cryptoUtility;
		}
}
