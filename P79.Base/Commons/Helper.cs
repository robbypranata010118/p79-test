using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Commons
{
    public static class Helper
    {
        public static string GenerateClientSecret(int length)
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[length];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string uniq = Convert.ToBase64String(buffer);
            return uniq;
        }
        public static string GenerateClientSecret()
        {
            return GenerateClientSecret(20);
        }

        public static string GetRandAlphanumericInLowAndUp(int length)
        {
            return RandomString(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        }


        public static string RandomString(int length, string chars)
        {
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string DisplayName<T>(string propertyName)
        {
            MemberInfo property = typeof(T).GetProperty(propertyName);
            return property.GetCustomAttribute<DisplayAttribute>()?.Name;
        }
        public static string GetRemoteSelectValue(string value)
        {
            return value != null ? value.Split("|")[0] : "";
        }
        public static string GetRemoteSelectLabel(string value)
        {
            return value != null ? value.Split("|").Length > 0 ? value.Split("|")[1] : value : "";
        }
        public static int GetRandNumeric(int length)
        {
            return Convert.ToInt32(RandomString(length, "0123456789"));
        }

        public static string GenerateCodeSubmission(string lastCode, string category)
        {
            var GenerateCode = string.Empty;
            var DateNow = DateTime.Now.Date;
            var GetDataLastCode = string.Empty;

            if (!string.IsNullOrEmpty(GetDataLastCode))
            {
                string[] tmpDataCode = lastCode.Split('/');
                var nextCode = int.Parse(tmpDataCode[2]) + 1;
                GenerateCode = string.Format("{0}/{1}/{2}", category, DateNow.ToString("yyyyMMdd"), nextCode.ToString().PadLeft(6, '0'));
            }
            else
            {
                GenerateCode = string.Format("{0}/{1}/{2}", category, DateNow.ToString("yyyyMMdd"), "000001");
            }

            return GenerateCode;
        }
    }
    
}
