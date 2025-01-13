using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Net.Mail;
using System.Net;

namespace Fee_Management.Admin
{
    public partial class add_student : System.Web.UI.Page
    {
            string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=Fee Management;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SelectAllCourses", connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        courseDetails.Items.Clear(); // Clear the dropdown list before repopulating

                        while (reader.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = reader["course"].ToString();
                            item.Value = reader["course"].ToString();

                            courseDetails.Items.Add(item);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in Page_Load: {ex.Message}");
                }
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            string generatedPassword = GenerateRandomPassword(8);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertStudentInformation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@name", studentName.Text);
                    command.Parameters.AddWithValue("@email", email.Text);
                    command.Parameters.AddWithValue("@phone_number", phoneNumber.Text);
                    command.Parameters.AddWithValue("@course", courseDetails.SelectedValue);
                    command.Parameters.AddWithValue("@address", address.Text);
                    command.Parameters.AddWithValue("@fee_waiver", CheckBox1.Checked ? 1 : 0);

                    // Convert FeeWaiverTextBox.Text to decimal before passing to the stored procedure
                    if (decimal.TryParse(FeeWaiverTextBox.Text, out decimal feeWaiverPercentage))
                    {
                        command.Parameters.AddWithValue("@fee_waiver_percentage", feeWaiverPercentage);
                    }
                    else
                    {
                        // If CheckBox1 is unchecked and no fee waiver percentage provided, set the parameter to DBNull.Value
                        command.Parameters.AddWithValue("@fee_waiver_percentage", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@password", generatedPassword);

                    command.ExecuteNonQuery();
                }
            }

            string recipientEmail = email.Text;
            SendPasswordEmail(recipientEmail, generatedPassword);

            Label1.Text = "Data inserted, and password sent to your email. Check your email.";
            Label1.ForeColor = System.Drawing.Color.Green;

            studentName.Text = string.Empty;
            email.Text = string.Empty;
            phoneNumber.Text = string.Empty;
            address.Text = string.Empty;
            CheckBox1.Checked = false;
            FeeWaiverTextBox.Text = string.Empty;
        }
        private string GenerateRandomPassword(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            Random random = new Random();
            string password = new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }
        private void SendPasswordEmail(string recipientEmail, string password)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mahetayash8@gmail.com"); 
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = "Password for Login";
            message.Body = "Your password is: " + password;
            message.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("mahetayash8@gmail.com", "twsl vkzh xwvz wosh");

            smtp.Send(message);
        }

        protected void email_TextChanged(object sender, EventArgs e)
        {
            string emaill = email.Text;
            if (IsEmailAlreadyRegistered(emaill))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "emailExistenceAlert", "alert('Email already exists. Please use a different email.');", true);
                email.Text = ""; 
            }
        }
        private bool IsEmailAlreadyRegistered(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CheckEmailExistence", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);

                    int result = (int)command.ExecuteScalar();
                    return result > 0; 
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            FeeWaiverTextBox.Visible= true; 
        }
    }
}