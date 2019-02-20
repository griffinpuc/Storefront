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

        public static List<bool> Validate(string uname, string pass)
        {

            List<LogInfo> CredList = Get();

            List<bool> Return;

            foreach (LogInfo i in CredList)
            {
                if ((ComputeHash(uname) == i.uname) && (ComputeHash(pass) == i.pass))
                {
                    Return = new List<bool>() {true, i.permission};
                    return Return;
                }
            }

            Return = new List<bool>() { false, false };
            return Return;
        }

        public static List<LogInfo> Get()
        {

            List<LogInfo> CredList = new List<LogInfo>();
            using (SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Database=DBCreds;Trusted_Connection=True;MultipleActiveResultSets=true"))
            using (SqlCommand cmd = new SqlCommand("SELECT ID AS ID, uname, pass, permission FROM DBCreds", connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            LogInfo i = new LogInfo();
                            i.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            i.uname = reader.GetString(reader.GetOrdinal("uname"));
                            i.pass = reader.GetString(reader.GetOrdinal("pass"));
                            i.permission = reader.GetBoolean(reader.GetOrdinal("permission"));
                            CredList.Add(i);
                        }
                    }
                }

                return CredList;
            }
            

        }

    }
}
