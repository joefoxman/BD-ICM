using System;

namespace Bd.Icm
{
    public static class Extensions
    {
        public static void ThrowIfNull(this object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty(this string input, string paramName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(paramName);
        }
    }
}
