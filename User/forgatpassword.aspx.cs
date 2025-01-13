using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using System.Data.SqlClient;

namespace Fee_Management.User
{
    public partial class forgatpassword : System.Web.UI.Page
    {
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True";
        public static int verificationCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        protected void btnotp_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the email from the input field
                string userEmail = email.Text.Trim(); ;

                // Check if the email exists in the student_information table
                if (IsEmailExists(userEmail))
                {
                    // Generate a random 6-digit code
                    Random random = new Random();
                    verificationCode = random.Next(100000, 999999);

                    // Send the verification code via email
                    SendVerificationEmail(userEmail);

                    Label2.Text = "Verification code sent. Check your email.";
                    Label2.ForeColor = System.Drawing.Color.Green;
                    
                    btnotp.Visible = false;
                    txtopt.Visible = true;
                    btnverify.Visible = true;
                }
                else
                {
                    Label2.Text = "Email does not exist.";
                    Label2.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Label2.Text = "Message Failed: " + ex.Message;
                Label2.ForeColor = System.Drawing.Color.Red;
            }
    }

        private string RetrievePassword(string userEmail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT password FROM student_information WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", userEmail);
                    object result = command.ExecuteScalar();
                    return (result != null) ? result.ToString() : "Password not found";
                }
            }
        }
        private bool IsEmailExists(string useremail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM student_information WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", useremail);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void SendVerificationEmail(string recipientEmail)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mahetayash8@gmail.com");
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = "Email Verification Code";
            message.Body = "Your verification code is: " + verificationCode;
            message.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("mahetayash8@gmail.com", "twsl vkzh xwvz wosh");

            smtp.Send(message);
        }

        protected void btnverify_Click(object sender, EventArgs e)
        {
            try
            {
                int enteredCode;
                if (int.TryParse(txtopt.Text, out enteredCode))
                {
                    if (enteredCode == verificationCode)
                    {
                        // Verification Successful
                        // Retrieve the password from the student_information table
                        string userEmail = email.Text;
                        string password = RetrievePassword(userEmail);

                        // Show the password in a popup or any other way you prefer
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Your password is: " + password + "');", true);

                        Label2.Text = "Verification Successful";
                        Label2.ForeColor = System.Drawing.Color.Green;

                        email.Text = string.Empty;
                        txtopt.Text = string.Empty;
                    }
                    else
                    {
                        Label2.Text = "Verification Code is Incorrect. Please try again.";
                        Label2.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    Label2.Text = "Invalid Verification Code Format. Please enter a 6-digit number.";
                    Label2.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Label2.Text = "Error: " + ex.Message;
                Label2.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}