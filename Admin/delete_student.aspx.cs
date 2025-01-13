using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.Admin
{
    public partial class delete_student : System.Web.UI.Page
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

                                deletestudent.Items.Add(item);
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
        protected void show_student_Click(object sender, EventArgs e)
        {
            string selectedCourse = deletestudent.SelectedValue;

            if (!string.IsNullOrEmpty(selectedCourse))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("GetStudentsByCourse", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@SelectedCourse", selectedCourse);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable studentTable = new DataTable();
                            adapter.Fill(studentTable);

                            if (studentTable.Rows.Count > 0)
                            {
                                ErrorMessage.Text = ""; // Clear error message
                                StudentRepeater.DataSource = studentTable;
                                StudentRepeater.DataBind();
                            }
                            else
                            {
                                ErrorMessage.Visible = true;
                                ErrorMessage.Text = "No students found for the selected course.";
                                StudentRepeater.DataSource = null;
                                StudentRepeater.DataBind();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }
            else
            {
                ErrorMessage.Text = "Please select a course to show students.";
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string email = btn.CommandArgument;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteStudentByEmail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}