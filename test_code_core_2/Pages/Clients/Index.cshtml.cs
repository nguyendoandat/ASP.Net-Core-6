using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test_code_core_2.Pages.Clients
{

    
    public class IndexModel : PageModel
    {

        public List<ClientInfo> listclient = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionstring = "Data Source=LAPTOP-TIPLB6VL\\SQLEXPRESS06;Initial Catalog=mystore;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo client  =new ClientInfo();
                                client.id = "" + reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.phone = reader.GetString(3);
                                client.address = reader.GetString(4);
                                client.created_at = reader.GetDateTime(5).ToString();


                                listclient.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
