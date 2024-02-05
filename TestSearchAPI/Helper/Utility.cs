using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#pragma warning disable
namespace TestSearchAPI.Helper
{
    public class Utility
    {
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, emailPattern);
        }

        public static bool VerifyPassword(string userpwd, string UIpassword)
        {
            return true;
        }
    }
}
