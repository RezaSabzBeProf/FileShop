using System.Security.Cryptography;
using System.Text;

namespace AspCore_Course
{
    public static class Password_helper
    {
        public static string EncodePassword(this string s)
        {
            StringBuilder sb = new StringBuilder();
            SHA256 hash = SHA256Managed.Create();
            Encoding enc = Encoding.UTF8;
            byte[] hashbyte = hash.ComputeHash(enc.GetBytes(s));
            foreach (byte b in hashbyte)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
