using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace Main.Models
{
    public class Validation
    {

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

            List<LogInfo> CredList = Get();

            foreach (LogInfo i in CredList)
            {
                if ((ComputeHash(uname) == i.uname) && (ComputeHash(pass) == i.pass))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<LogInfo> Get()
        {

            List<LogInfo> CredList = new List<LogInfo>();
            using (SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Database=DBCreds;Trusted_Connection=True;MultipleActiveResultSets=true"))
            using (SqlCommand cmd = new SqlCommand("SELECT ID AS ID, uname, pass FROM DBCreds", connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            LogInfo i = new LogInfo();
                            // To avoid unexpected bugs access columns by name.
                            i.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            i.uname = reader.GetString(reader.GetOrdinal("uname"));
                            i.pass = reader.GetString(reader.GetOrdinal("pass"));
                            CredList.Add(i);
                        }
                    }
                }

                return CredList;
            }
            

        }

    }
}
