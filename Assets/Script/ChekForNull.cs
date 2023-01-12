#nullable enable
using System;

public static class ChekForNull
{
    public static T EnsureNotNull<T>(this T? value)
    {
        if (value == null) throw new NullReferenceException(typeof(T).FullName);
        
        if (value == null) throw new NullReferenceException();

        return value;
    }
}