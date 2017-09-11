using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MemoryGame
{
    class Database
    {
        public static BindingSource bindingSource1 = new BindingSource();


        string myConn = @"Data Source=AMPIAPPDEV1\DEV1;Initial Catalog=Memory;MultipleActiveResultSets=true;user id=sa; password=DEV!Admin";
        public void Insert(string Name, string time)
        {
            string sql = "Insert into Highscore ([Name], [Time]) values (@name, @time)";
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public string read()
        {
            string result = "";
            string sql = "Select COALESCE(Min(Time),0) as Time From Highscore";
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = reader.GetValue(0).ToString();
                    }
                }
                con.Close();
                return result;
            }
        }

        public void selectScore()
        {
            string result = "";
            string sql = "Select TOP 3 * From Highscore order by time ASC";
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlDataAdapter cmd = new SqlDataAdapter(sql, con))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(cmd);


                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    cmd.Fill(table);
                    bindingSource1.DataSource = table;
                }
                con.Close();
            }
        }
    }


}
