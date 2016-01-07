using System;
using System.Security.Cryptography;
using System.Text;

// TODO: Should check security algorithms being used to make sure they provide high enough cryptography difficulties

namespace IrisWeb.Code.Data.Helpers
{
    public static class CryptoHelper
    {
        public static void ComputePassword(string plainPassword, out string passwordHash, out string passwordSalt)
        {
            passwordSalt = GenerateSalt();
            passwordHash = ComputeHash(plainPassword, passwordSalt);
        }

        public static string GenerateSalt()
        {
            byte[] data = new byte[66];
            new RNGCryptoServiceProvider().GetBytes(data);

            return Convert.ToBase64String(data);
        }

        public static string ComputeHash(string plainPassword, string passwordSalt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainPassword);
            byte[] saltBytes = Convert.FromBase64String(passwordSalt);

            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 5))
            {
                byte[] hashedPasswordBytes = hasher.GetBytes(129);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }
    }
}