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
    public partial class update_class : System.Web.UI.Page
    {
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=Fee Management;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmail"] != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SelectAllCourses", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                ListItem item = new ListItem();
                                item.Text = reader["course"].ToString();
                                item.Value = reader["course"].ToString();

                                currentClass.Items.Add(item);
                                newClass.Items.Add(item);
                            }

                            reader.Close();
                        }
                    }
                }

                else
                {
                    Response.Redirect("admin_login.aspx");
                }
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string selectedCurrentCourse = currentClass.SelectedValue;
            string selectedNewCourse = newClass.SelectedValue;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UpdateStudentCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdateStudentCourse";

                        command.Parameters.AddWithValue("@NewCourse", selectedNewCourse);
                        command.Parameters.AddWithValue("@CurrentCourse", selectedCurrentCourse);

                        SqlParameter successParameter = new SqlParameter("@Success", SqlDbType.Int);
                        successParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(successParameter);

                        int rowsAffected = command.ExecuteNonQuery();
                        int success = Convert.ToInt32(successParameter.Value);

                        if (success == 1)
                        {
                            Label3.Text = "Course updated successfully";
                            Label3.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label3.Text = "Course not updated or not found";
                            Label3.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "An error occurred: " + ex.Message;
                Label3.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}