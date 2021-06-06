/* Copyright (C) 2012, 2013 Dan Leonard
 * This file is part of DMP Management App.
 * 
 * DMP Management App is free software: you can redistribute it and/or 
 * modify it under the terms of the GNU General Public License as published 
 * by the Free Software Foundation, either version 3 of the License, or (at
 * your option) any later version.
 * 
 * DMP Management App is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for 
 * more details.
 */

using Common.Models.Authentication.User.Abstractions;
using Common.Utilities.Authentication.Jwt;
using Common.Utilities.Cryptography;
using Common.Utilities.UserManagement.Data;
using Common.Utilities.UserManagement.Exceptions;
using Common.Utilities.UserManagement.Extensions;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Common.Utilities.UserManagement.Components
{
		public interface IUserManagementComponent
		{
				Task<string> AuthenticateUser(IUserModel user);
				Task<bool> CreateUser(IUserModel user);
				Task<bool> DeleteUser(IUserModel userModel);
		}

		public class UserManagementComponent : IUserManagementComponent
		{
				public UserManagementComponent(IJwtTokenUtility jwtTokenUtility,
						IUserRepository userRepository,
						ICryptoUtility cryptoProvider)
				{
						_jwtTokenUtility = jwtTokenUtility ?? throw new ArgumentNullException(nameof(jwtTokenUtility));
						_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
						_cryptoUtility = cryptoProvider ?? throw new ArgumentNullException(nameof(cryptoProvider));
				}

				public async Task<string> AuthenticateUser(IUserModel user)
				{
						var userId = await _userRepository.GetUserId(user.Username);

						if (userId == null)
						{
								throw new UserException($"User {user} was not found.");
						}

						var dbUser = await _userRepository.GetUser((int)userId);

						var comparisonHash = _cryptoUtility.GenerateSaltedHash(user.Password, dbUser.Salt);

						if (comparisonHash == dbUser.Password)
						{
								var userToken = _jwtTokenUtility.GetToken(dbUser);

								return userToken;
						}

						throw new AuthenticationException($"Incorrect password for user {user.Username}");
				}

				public async Task<bool> CreateUser(IUserModel user)
				{
						var userId = await _userRepository.GetUserId(user.Username);

						if (userId != null)
						{
								throw new UserException($"{user.Username} is not an available username.");
						}

						var password = _cryptoUtility.GenerateSaltedHash(user.Password);

						user.HashPassword(password);

						var result = await _userRepository.InsertUser(user);

						if (result < 1)
						{
								throw new UserException($"Failed to create user {user.Username}");
						}

						return true;
				}

				public async Task<bool> DeleteUser(IUserModel userModel)
				{
						var userId = await _userRepository.GetUserId(userModel.Username);

						if (userId == null)
						{
								throw new UserException($"User {userModel.Username} was not found.");
						}

						var result = await _userRepository.DeleteUser((int)userId);

						if (result < 1)
						{
								throw new UserException($"Failed to delete {userModel.Username}.");
						}

						return true;
				}

				private readonly ICryptoUtility _cryptoUtility;
				private readonly IUserRepository _userRepository;
				private readonly IJwtTokenUtility _jwtTokenUtility;
		}

}
