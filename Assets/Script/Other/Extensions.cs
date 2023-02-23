namespace Script.Other
{
    public static class Extensions
    {
        public static T Cache<T>(this T value, out T cached)
        {
            cached = value;
            return value;
        }
    }
}