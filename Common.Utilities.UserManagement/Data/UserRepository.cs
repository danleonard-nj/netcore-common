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
using Common.Utilities.UserManagement.Models;
using Common.Utilities.UserManagement.Settings;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Utilities.UserManagement.Data
{
		public interface IUserRepository
		{
				Task<int> DeleteUser(int userId);
				Task<IUserModel> GetUser(int userId);
				Task<int?> GetUserId(string username);
				Task<int> InsertUser(IUserModel user);
		}

		public class UserRepository : IUserRepository
		{
				public UserRepository(UserManagementSettings settings)
				{
						_settings = settings ?? throw new ArgumentNullException(nameof(settings));
				}

				public async Task<IUserModel> GetUser(int userId)
				{
						var sql = @"
								SELECT UserId
									,Username
									,Email
									,[Password]
									,Salt
									,[Role]
								FROM dbo.[User]
								WHERE UserId = @UserId";

						using (var connection = new SqlConnection(_settings.SqlConnectionString))
						{
								var user = await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { userId });

								return user;
						}
				}

				public async Task<int> InsertUser(IUserModel user)
				{
						var sql = @"
								INSERT dbo.[User] (
									Username
									,Email
									,[Password]
									,Salt
									,[Role]
									,CreatedDate)
								VALUES (
									@Username
									,@Email
									,@Password
									,@Salt
									,@Role
									,GETDATE())";

						using (var connection = new SqlConnection(_settings.SqlConnectionString))
						{
								var result = await connection.ExecuteAsync(sql, user);

								return result;
						}
				}

				public async Task<int> DeleteUser(int userId)
				{
						var sql = @"
								DELETE FROM dbo.[User]
								WHERE UserId = @UserId";

						using (var connection = new SqlConnection(_settings.SqlConnectionString))
						{
								var result = await connection.ExecuteAsync(sql, new { userId });

								return result;
						}
				}

				public async Task<int?> GetUserId(string username)
				{
						var sql = @"
								SELECT UserId
								FROM dbo.[User]
								WHERE Username = @Username";

						using (var connection = new SqlConnection(_settings.SqlConnectionString))
						{
								var userId = await connection.QuerySingleOrDefaultAsync<int?>(sql, new { username });

								return userId;
						}
				}

				private readonly UserManagementSettings _settings;
		}
}
