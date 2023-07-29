using System.Security.Cryptography;
using System.Text;

namespace CXChange.Modules.UserManagment.Infrastructure.Utils
{
    internal static class ExtensioinMethods
    {
        public static string ComputeSha256Hash(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            using (System.Security.Cryptography.SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static string GenerateRandomToken(this int length)
        {

            var randomNumber = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                // return Convert.ToHexString(randomNumber);

                return Convert.ToBase64String(randomNumber).Replace("=", "").Replace("+", "").Replace("/", "");

            }
        }
    }
}
