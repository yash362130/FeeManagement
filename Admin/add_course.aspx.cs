using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.Admin
{
    public partial class add_course : System.Web.UI.Page
    {
        private readonly string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminEmail"] == null)
            {
                Response.Redirect("admin_login.aspx");
            }
        }

        private void InsertCourseIntoDatabase(string courseName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("InsertCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseName", courseName);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string messageText = reader["Message"].ToString();
                            message.Visible = true;
                            message.Text = messageText;
                            message.ForeColor = messageText == "Data Inserted Successfully" ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                        }
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        message.Visible = true;
                        message.Text = ex.Message;
                        message.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        protected void add_Click(object sender, EventArgs e)
        {
            string courseName = addcourse.Text;

            if (!string.IsNullOrEmpty(courseName))
            {
                InsertCourseIntoDatabase(courseName);
            }
            else
            {
                // Handle empty course name
                // For example: display an error message
            }
        }
    }
}