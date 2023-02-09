#nullable enable
using System;

namespace Script.Other
{
    public static class ChekForNull
    {
        public static T EnsureNotNull<T>(this T? value, string? message = null)
        {
            if (value == null) throw new NullReferenceException(message ?? typeof(T).FullName);
            if (value == null) throw new NullReferenceException();
        
            return value;
        }
    }
}