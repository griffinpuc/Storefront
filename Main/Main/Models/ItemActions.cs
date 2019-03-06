using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class ItemActions : IItemRepo
    {

        public List<Item> Get()
        {

            List<Item> ItemList = new List<Item>();
            using (SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Database=DBCreds;Trusted_Connection=True;MultipleActiveResultSets=true"))
            using (SqlCommand cmd = new SqlCommand("SELECT ID AS ID, Code, Name, Descr, WPrice, Price, Quantity, Category FROM DBItems", connection))
            {

                Item test = new Item
                {
                    Code = 1,
                    Name = "Plate",
                    Descr = "A normal plate",
                    WPrice = 9.30,
                    Price = 15.99,
                    Quantity = 150,
                    Category = "Kitchen"

                };

                connection.Open();


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Item i = new Item();
                            i.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            i.Name = reader.GetString(reader.GetOrdinal("Name"));
                            i.Code = reader.GetInt32(reader.GetOrdinal("Code"));
                            i.Descr = reader.GetString(reader.GetOrdinal("Descr"));
                            i.WPrice = reader.GetDouble(reader.GetOrdinal("WPrice"));
                            i.Price = reader.GetDouble(reader.GetOrdinal("Price"));
                            i.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                            i.Category = reader.GetString(reader.GetOrdinal("Category"));
                            ItemList.Add(i);
                        }
                    }

                    connection.Close();

                    return ItemList;
                }
            }


        }

        public IQueryable<Item> Items => Get().AsQueryable<Item>();

        public Item GetItems(int id)
        {

            foreach(Item item in Items)
            {
                if(id == item.Code)
                {
                    return item;
                }
            }

            return null;
        }

        public bool AddItem(Item item)
        {

            using (SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Database=DBCreds;Trusted_Connection=True;MultipleActiveResultSets=true"))
            using (SqlCommand cmd = new SqlCommand("SELECT ID AS ID, Code, Name, Descr, WPrice, Price, Quantity, Category FROM DBItems", connection))
            
            using (connection)
            {
                String query = "INSERT INTO DBItems (Name,Code,Descr,WPrice,Price,Quantity,Category) VALUES (@2,@3,@4,@5,@6,@7,@8)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@2", item.Name);
                    command.Parameters.AddWithValue("@3", item.Code);
                    command.Parameters.AddWithValue("@4", item.Descr);
                    command.Parameters.AddWithValue("@5", item.WPrice);
                    command.Parameters.AddWithValue("@6", item.Price);
                    command.Parameters.AddWithValue("@7", item.Quantity);
                    command.Parameters.AddWithValue("@8", item.Category);


                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        connection.Close();

                        return false;
                    }

                    connection.Close();

                    return true;
                }
            }
        }

        public bool DelItem(string code)
        {

            SqlConnection connection = new SqlConnection("server=(localdb)\\mssqllocaldb;Database=DBCreds;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand cmd = new SqlCommand("DELETE FROM DBItems WHERE CODE = @1", connection);

            cmd.Parameters.AddWithValue("@1", int.Parse(code));

            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            return true;
        }
    }
}
