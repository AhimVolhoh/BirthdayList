using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirthdayList.Pages.folks
{
    public class EditModel : PageModel
    {
        public BirthdayInfo birthdayInfo = new BirthdayInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"]; 

            try
            {
                String connectionString = "Data Source=laptop-mihhail;Initial Catalog=BirthdayList;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM folks WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                birthdayInfo.id = "" + reader.GetInt32(0);
                                birthdayInfo.name = reader.GetString(1);
                                birthdayInfo.Birthday_Date = reader.GetString(2);

                            }
                        }
                    }
                }
            }

            catch (Exception ex) 
            {
                errorMessage= ex.Message;
            }
        }

        public void OnPost() 
        {
            birthdayInfo.id = Request.Form["id"];
            birthdayInfo.name = Request.Form["name"];
            birthdayInfo.Birthday_Date = Request.Form["Birthday_Date"];

            if(birthdayInfo.id.Length == 0 || birthdayInfo.name.Length == 0 || 
                birthdayInfo.Birthday_Date.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=laptop-mihhail;Initial Catalog=BirthdayList;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE folks " + "SET name=@name, Birthday_Date=@Birthday_Date " + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
						command.Parameters.AddWithValue("@id", birthdayInfo.id);
						command.Parameters.AddWithValue("@name", birthdayInfo.name);
                        command.Parameters.AddWithValue("@Birthday_Date", birthdayInfo.Birthday_Date);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/folks/Index");
        }
    }
}
