using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.User
{
    public partial class login : System.Web.UI.Page
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
                    string query = "SELECT COUNT(*) FROM student_information WHERE Email = @Email AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", inputEmail);
                        command.Parameters.AddWithValue("@Password", inputPassword);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            string userInfoQuery = "SELECT  Name, Email, Course, Phone_number, Address FROM student_information WHERE Email = @Email";

                            using (SqlCommand userInfoCommand = new SqlCommand(userInfoQuery, connection))
                            {
                                userInfoCommand.Parameters.AddWithValue("@Email", inputEmail);

                                using (SqlDataReader reader = userInfoCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        Session["UserName"] = reader["Name"].ToString();
                                        Session["Useremail"] = reader["Email"].ToString();
                                        Session["UserCourse"] = reader["Course"].ToString();
                                        Session["UserPhoneNumber"] = reader["Phone_number"].ToString();
                                        Session["UserAddress"] = reader["Address"].ToString();
                                    }
                                }
                            }
                            Response.Redirect("user_information.aspx");
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
            }
            catch (Exception ex)
            {
                Label1.Text += ex.Message;
            }
        }
    }
}
