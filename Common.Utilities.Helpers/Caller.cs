/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
