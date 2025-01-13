using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.Admin
{
    public partial class add_fee : System.Web.UI.Page
    {
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;MultipleActiveResultSets=true";
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
                            }

                            reader.Close();
                        }
                    }
                    txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

                else
                {
                    Response.Redirect("admin_login.aspx");
                }
            }
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            txtLastDate.Text = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
        }


        protected void btnAddFee_Click(object sender, EventArgs e)
        {
            string feeDescription = txtFeeDescription.Text;
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            string course = currentClass.SelectedValue;
            DateTime declareDate = Convert.ToDateTime(txtCurrentDate.Text);
            DateTime lastDate = Convert.ToDateTime(txtLastDate.Text);
            string status = "Pending";

            // Check if the checkbox is selected
            bool applyDiscount = !chkNoDiscount.Checked;

            List<string> studentEmails = GetEnrolledStudentsEmails(course); // Utilize the GetEnrolledStudentsEmails method to get student emails

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Iterate through students for fee insertion
                using (SqlCommand command = new SqlCommand("SelectStudentInformationByCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Course", course);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string studentName = reader["name"].ToString();
                        string phoneNumber = reader["phone_number"].ToString();
                        string email = reader["email"].ToString();

                        // Handle DBNull for fee_waiver and fee_waiver_percentage
                        int feeWaiver = reader["fee_waiver"] != DBNull.Value ? Convert.ToInt32(reader["fee_waiver"]) : 0;
                        decimal feeWaiverPercentage = reader["fee_waiver_percentage"] != DBNull.Value ? Convert.ToDecimal(reader["fee_waiver_percentage"]) : 0;

                        decimal discountedAmount = applyDiscount ? amount - (amount * (feeWaiverPercentage / 100)) : amount;

                        AddFeeDetails(connection, studentName, phoneNumber, email, course, feeDescription, discountedAmount, declareDate, lastDate, status);
                    }
                    reader.Close();
                }
            }

            SendEmailNotifications(studentEmails, feeDescription, course, lastDate, amount);
            Label1.Text = "Fee details added successfully!";
            Label1.ForeColor = System.Drawing.Color.Green;
        }

        private void AddFeeDetails(SqlConnection connection, string studentName, string phoneNumber, string email, string course, string feeDescription, decimal amount, DateTime declareDate, DateTime lastDate, string status)
        {
            string insertProcedure = "InsertFeeDetails";

            using (SqlCommand insertCommand = new SqlCommand(insertProcedure, connection))
            {
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@StudentName", studentName);
                insertCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Course", course);
                insertCommand.Parameters.AddWithValue("@FeeDescription", feeDescription);
                insertCommand.Parameters.AddWithValue("@Amount", amount);
                insertCommand.Parameters.AddWithValue("@DeclareDate", declareDate);
                insertCommand.Parameters.AddWithValue("@LastDate", lastDate);
                insertCommand.Parameters.AddWithValue("@Status", status);

                insertCommand.ExecuteNonQuery();
            }
        }


        public class StudentData
        {
            public string StudentName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public int FeeWaiver { get; set; }
        }

        private List<string> GetEnrolledStudentsEmails(string courseDetails)
        {
            List<string> enrolledStudentsEmails = new List<string>();
            string selectProcedure = "SelectEnrolledStudentsEmailByCourse";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(selectProcedure, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CourseDetails", courseDetails);

                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            string email = reader["Email"].ToString();
                            enrolledStudentsEmails.Add(email);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Label1.Text = ex.Message;
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }

            return enrolledStudentsEmails;
        }

        private void SendEmailNotifications(List<string> emails, string feeDescription, string courseDetails, DateTime lastDate, decimal amount)
        {
            foreach (string email in emails)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("mahetayash8@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Fee Added Notification";
                    mail.Body = $"Fee of  {feeDescription} added for course {courseDetails}. " +
                                $"The amount is {amount}. " +
                                $"Please note that the fee payment deadline {lastDate} for {courseDetails} is approaching. " +
                                $"Ensure timely submission to avoid any interruptions in your academic journey. " +
                                $"Your cooperation is appreciated.";

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("mahetayash8@gmail.com", "twsl vkzh xwvz wosh");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                }
                catch (SmtpException smtpEx)
                {
                    Label2.Text = "SMTP Error: " + smtpEx.Message;
                    Label2.ForeColor = System.Drawing.Color.Red;
                    // Log the detailed error for debugging: smtpEx.ToString();
                }
                catch (Exception ex)
                {
                    Label2.Text = "Error sending email: " + ex.Message;
                    Label2.ForeColor = System.Drawing.Color.Red;
                    // Log the detailed error for debugging: ex.ToString();
                }
            }
        }
    }
}

