using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.Admin
{
    public partial class admin_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True";
            string inputEmail = email.Text;
            string inputPassword = password.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Using a stored procedure to validate login
                    SqlCommand command = new SqlCommand("ValidateAdminLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@Email", inputEmail);
                    command.Parameters.AddWithValue("@Password", inputPassword);

                    // Output parameter for count
                    SqlParameter countParam = new SqlParameter("@Count", SqlDbType.Int);
                    countParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(countParam);

                    // Execute the command
                    command.ExecuteNonQuery();

                    // Get the count from the output parameter
                    int count = (int)countParam.Value;

                    if (count > 0)
                    {
                        Session["AdminEmail"] = inputEmail;
                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        email.Text = string.Empty;
                        password.Text = string.Empty;
                        Label1.Text = "Wrong email or password";
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}