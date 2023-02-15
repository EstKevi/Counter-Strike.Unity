#nullable enable
using System;

namespace Script.Other
{
    public static class CheckForNull
    {
        public static T EnsureNotNull<T>(this T? value, string? message = null)
        {
            if (value != null) return value;
            throw new NullReferenceException(message ?? typeof(T).FullName);
            throw new NullReferenceException();
        }
    }
}