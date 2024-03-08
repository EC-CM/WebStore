using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

using BCrypt.Net;

namespace WebStore.Services
{
    public static class PasswordHasher
    {
        private const int WorkFactor = 10;

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
