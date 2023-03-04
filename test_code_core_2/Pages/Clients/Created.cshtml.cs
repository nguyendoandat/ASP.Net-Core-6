using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace test_code_core_2.Pages.Clients
{
    public class CreatedModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";

        public string SuccessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0
                || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                ErrorMessage = "All the fields are requied";
                return;
            }

            try
            {
                String connectionstring = "Data Source=LAPTOP-TIPLB6VL\\SQLEXPRESS06;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" + "(name,email,phone,address) VALUES" + "(@name,@email,@phone,@address)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;

            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            SuccessMessage = "New Client created corectly";


            Response.Redirect("/Clients/Index");
        }
    }
}
