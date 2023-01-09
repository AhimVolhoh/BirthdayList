using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirthdayList.Pages.folks
{
    public class IndexModel : PageModel
    {
        public List<BirthdayInfo> BirthdayList = new List<BirthdayInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=laptop-mihhail;Initial Catalog=BirthdayList;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM folks";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
						using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BirthdayInfo birthdayInfo = new BirthdayInfo();

                                birthdayInfo.id = "" + reader.GetInt32(0);
								birthdayInfo.name = reader.GetString(1);
								birthdayInfo.Birthday_Date = reader.GetString(2);
								birthdayInfo.created_at = reader.GetDateTime(3).ToString();

								BirthdayList.Add(birthdayInfo);
					  		}
                        }

					}
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }

    public class BirthdayInfo
    {
        public String id;
        public String name;
        public String Birthday_Date;
        public String created_at;
    }
}