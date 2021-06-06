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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Common.Utilities.Helpers
{
		[ExcludeFromCodeCoverage]
    public static class Caller
    {
        public static string GetMethodName([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
    }
}
