using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using P79.Base.Extensions;

namespace P79.Base.Extentions
{
    public static class StringExtension
    {
        /// <summary>
        /// Create new hash string using SHA256
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSHA256(this string s)
        {
            using (SHA256 shaManager = new SHA256Managed())
            {
                string hash = string.Empty;
                byte[] bytes = shaManager.ComputeHash(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));
                foreach (byte b in bytes)
                {
                    hash += b.ToString("x2");
                }

                return hash;
            }
        }

        /// <summary>
        /// Create new hash string using SHA512
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSHA512(this string s)
        {
            using (SHA512 shaManager = new SHA512Managed())
            {
                string hash = string.Empty;
                byte[] bytes = shaManager.ComputeHash(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetByteCount(s));
                foreach (byte b in bytes)
                {
                    hash += b.ToString("x2");
                }

                return hash;
            }
        }

        /// <summary>
        /// Create new hash string using MD5
        /// </summary>
        /// <remarks>
        /// Resource from https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        /// </remarks>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToMD5(this string s)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(s);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string Base64Encode(this string s)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string s)
        {
            var base64EncodedBytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToRADHashed(this string value, byte[] saltInBytes)
        {
            saltInBytes.ThrowIfNullOrDefault();

            string salt = Encoding.UTF8.GetString(saltInBytes);
            salt.ThrowIfNullOrDefault();
            if (salt.Length != 64)
                throw new ArgumentException("Salt must be 64 length");

            string[] parts = { salt.Substring(0, (64 / 2) - 2), salt.Substring((64 / 2) - 2) };
            parts[0] = parts[0].ReverseString();
            value = string.Concat(parts[1], value, parts[0]);
            return value.ToSHA512();
        }

    }
}
