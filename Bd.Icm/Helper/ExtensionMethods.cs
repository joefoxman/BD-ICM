using System;
using System.ComponentModel;

namespace Bd.Icm.Helper
{
    internal static class ExtensionMethods
    {
        public static string Hash(this string chars)
        {
            var enc = new Encryption();
            return enc.HashPassword(chars);
        }
        public static string Encrypt(this string chars, string secretKey = "")
        {
            var enc = new Encryption { SecretKey = secretKey };
            return enc.Encode(chars);
        }
        public static string Decrypt(this string chars, string secretKey = "")
        {
            var enc = new Encryption { SecretKey = secretKey };
            return enc.Decode(chars);
        }
        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}
