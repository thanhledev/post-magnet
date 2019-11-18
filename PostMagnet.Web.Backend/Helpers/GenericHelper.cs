using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PostMagnet.Web.Backend.Helpers
{
    public class GenericHelper
    {
        public static string GenerateSlug(int id, string name)
        {
            string phrase = string.Format("{0}-{1}", id, name);

            string str = RemoveAccent(phrase).ToLower();

            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s+", "-"); //hyphens

            return str;
        }

        private static string RemoveAccent(string text)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return Encoding.ASCII.GetString(bytes);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
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

        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            foreach (TimeZoneInfo info in TimeZoneInfo.GetSystemTimeZones())
            {
                if (String.Compare(info.Id, timeZoneId, StringComparison.OrdinalIgnoreCase) == 0)
                    return info;
            }
            return TimeZoneInfo.Utc;
        }
    }
}