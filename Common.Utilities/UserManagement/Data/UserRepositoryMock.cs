/* Copyright (C) 2021 Dan Leonard
 * 
 * This  is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */

using Common.Utilities.UserManagement.Models;
using Common.Utilities.UserManagement.Models.Interfaces;
using System.Threading.Tasks;

namespace Common.Utilities.UserManagement.Data
{
		public class UserRepositoryMock : IUserRepository
		{
				public async Task<int> DeleteUser(int userId)
				{
						await Task.Yield();

						return 1;
				}

				public async Task<IUserModel> GetUser(int userId)
				{
						await Task.Yield();

						return new UserModel();
				}

				public async Task<int?> GetUserId(string username)
				{
						await Task.Yield();

						return 1;
				}

				public async Task<int> InsertUser(IUserModel user)
				{
						await Task.Yield();

						return 1;
				}
		}
}
