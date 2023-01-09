using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirthdayList.Pages.folks
{
    public class CreateModel : PageModel
    {
        public BirthdayInfo birthdayInfo = new BirthdayInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            birthdayInfo.name= Request.Form["name"];
			birthdayInfo.Birthday_Date = Request.Form["Birthday_Date"];

            if(birthdayInfo.name.Length == 0 || birthdayInfo.Birthday_Date.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=laptop-mihhail;Initial Catalog=BirthdayList;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
					connection.Open();
					String sql = "INSERT INTO folks" +
						"(name,Birthday_Date) VALUES" +
						"(@name,@Birthday_Date);";


					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("name", birthdayInfo.name);
						command.Parameters.AddWithValue("Birthday_Date", birthdayInfo.Birthday_Date);

						command.ExecuteNonQuery();
					}
				}
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            birthdayInfo.name = "";birthdayInfo.Birthday_Date = "";
            successMessage = "New Folk Added Correctly";

            Response.Redirect("index");
		}    
    }
}
