using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Main.Models
{
    public class Validation
    {

        //Move to SQL
        static string username = ComputeHash("admin");
        static string password = ComputeHash("password");

        public static string ComputeHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static Boolean Validate(string uname, string pass)
        {
            if((ComputeHash(uname) == username) && (ComputeHash(pass) == password))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}
