using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


namespace Fee_Management.User
{
    public partial class user_information : System.Web.UI.Page
    {
        public string orderId;
        public string amount { get; set; }
        public string name { get; set; }
        public string Course { get; set; }

        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;Trusted_connection=True;Encrypt=False";
        protected void Page_Load(object sender, EventArgs e)
        {
            display_information();

            if (!IsPostBack)
            {
                if (Session["Useremail"] != null)
                {
                    string useremail = Session["Useremail"].ToString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"select * from fee_details where email=@Useremail";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Useremail", useremail);
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            connection.Close();

                            if (dataTable.Rows.Count > 0)
                            {
                                FeeReapter.DataSource = dataTable;
                                FeeReapter.DataBind();
                            }
                            else
                            {
                                
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
        public void display_information()
        {
            if (Session["UserEmail"] != null)
            {
                string userName = Session["UserName"].ToString();
                string userEmail = Session["Useremail"].ToString();
                string userCourse = Session["UserCourse"].ToString();
                string userPhoneNumber = Session["UserPhoneNumber"].ToString();
                string userAddress = Session["UserAddress"].ToString();

                lblName.Text = userName;
                lblEmail.Text = userEmail;
                lblCourse.Text = userCourse;
                lblPhoneNumber.Text = userPhoneNumber;
                lbladdress.Text = userAddress;
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void Pay_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            RepeaterItem clickedItem = (RepeaterItem)clickedButton.NamingContainer;

            Label amountLabel = clickedItem.FindControl("AmountLabel") as Label;
            string amountToPay = amountLabel.Text;

            Label pidLabel = clickedItem.FindControl("pid") as Label;
            string id = pidLabel.Text;

            if (Session["Useremail"] != null)
            {
                    if (decimal.TryParse(amountToPay, out decimal paymentAmount))
                    {
                        int amountInPaise = (int)(paymentAmount);
                        amount = (amountInPaise).ToString();

                        Response.Redirect(String.Format("checkout.aspx?Name={0}&Email={1}&amount={2}&Course={3}&ID={4}", lblName.Text, lblEmail.Text, amount, lblCourse.Text,id));
                    }
                    else
                    {
                        // Handle parsing amount error
                    }
            }
            else
            {
               Response.Redirect("login.aspx");
            }
        }

        protected void FeeReapter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    string status = drv["status"] as string;

                    Label statusLabel = e.Item.FindControl("statusLabel") as Label;

                    if (statusLabel != null)
                    {
                        statusLabel.Text = status;

                        if (status != null && status.Equals("paid", StringComparison.OrdinalIgnoreCase))
                        {
                            e.Item.Visible = false;
                        }
                        else if (status != null && status.Equals("pending", StringComparison.OrdinalIgnoreCase))
                        {
                            // Change the font color to orange for pending status
                            statusLabel.ForeColor = System.Drawing.Color.Orange;
                        }
                    }
                }
            }
        }

        protected void btnEditPassword_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showPayModalpassword", "showPayModalpassword();", true);
        }

        protected void btnEditEmail_Click(object sender, EventArgs e)
        {
            currentemail.Text = lblEmail.Text.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "showPayModal", "showPayModal();", true);
        }

        protected void btnSavePayment_Click(object sender, EventArgs e)
        {
            string oldEmail = currentemail.Text;
            string newEmail = txtemail.Text;
            string enteredPassword = txtpassword.Text;

            // Check if the new email is available
            if (IsEmailAvailable(newEmail))
            {
                // Retrieve the password associated with the old email
                string oldPassword = GetPasswordByEmail(oldEmail);

                // Check if the entered password matches the retrieved password
                if (oldPassword == enteredPassword)
                {
                    // Password is correct, update in student_information and fee_details tables
                    UpdateEmailInStudentInformation(oldEmail, newEmail);
                    UpdateEmailInFeeDetails(oldEmail, newEmail);

                    // Clear the textboxes and redirect
                    currentemail.Text = string.Empty;
                    txtemail.Text = string.Empty;
                    txtpassword.Text = string.Empty;
                    Response.Redirect("login.aspx");
                }
                else
                {
                    // Password is incorrect, display a message
                    Response.Write("Incorrect password. Please enter the correct password.");
                }
            }
            else
            {
                // Email is not available, display a message
                Response.Write("The entered email is already in use. Please choose a different email.");
            }
        }

        private void UpdateEmailInStudentInformation(string oldEmail, string newEmail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update email in the student_information table
                string updateStudentInformationQuery = "UPDATE student_information SET email = @NewEmail WHERE email = @OldEmail";

                using (SqlCommand command = new SqlCommand(updateStudentInformationQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewEmail", newEmail);
                    command.Parameters.AddWithValue("@OldEmail", oldEmail);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Additional method to update email in the fee_details table
        private void UpdateEmailInFeeDetails(string oldEmail, string newEmail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update email in the fee_details table
                string updateFeeDetailsQuery = "UPDATE fee_details SET email = @NewEmail WHERE email = @OldEmail";

                using (SqlCommand command = new SqlCommand(updateFeeDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewEmail", newEmail);
                    command.Parameters.AddWithValue("@OldEmail", oldEmail);

                    command.ExecuteNonQuery();
                }
            }
        }
        private bool IsEmailAvailable(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the email is available in the student_information table
                string checkEmailQuery = "SELECT COUNT(*) FROM student_information WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(checkEmailQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    // Use int.TryParse to handle potential null values
                    int count;
                    if (int.TryParse(command.ExecuteScalar()?.ToString(), out count))
                    {
                        // If count is equal to 0, email is available
                        return count == 0;
                    }
                    else
                    {
                        // Handle the case where the result couldn't be parsed as an integer
                        return false;
                    }
                }
            }
        }
        private string GetPasswordByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve the password associated with the email from the student_information table
                string getPasswordQuery = "SELECT password FROM student_information WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(getPasswordQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    // Use ExecuteScalar to retrieve a single value (password)
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }

        protected void btnpassword_Click(object sender, EventArgs e)
        {
            string email = lblEmail.Text; // Assuming lblEmail contains the current email
            string currentPassword = currentpassword.Text;
            string newPassword = newpassword.Text;

            // Check if the entered current password matches the password from the database
            if (IsPasswordCorrect(email, currentPassword))
            {
                // Password is correct, update the password in the student_information table
                UpdatePasswordInStudentInformation(email, newPassword);

                // Clear the textboxes and close the modal
                currentpassword.Text = string.Empty;
                newpassword.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ClosePasswordModal();", true);
            }
            else
            {
                // Password is incorrect, display a message or take appropriate action
                Response.Write("Incorrect current password. Please enter the correct password.");
            }
        }
        private bool IsPasswordCorrect(string email, string enteredPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve the password associated with the email from the student_information table
                string getPasswordQuery = "SELECT password FROM student_information WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(getPasswordQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    // Use ExecuteScalar to retrieve a single value (password)
                    string storedPassword = command.ExecuteScalar()?.ToString();

                    // Check if the entered password matches the stored password
                    return string.Equals(storedPassword, enteredPassword);
                }
            }
        }

        private void UpdatePasswordInStudentInformation(string email, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the password in the student_information table
                string updatePasswordQuery = "UPDATE student_information SET password = @NewPassword WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(updatePasswordQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@Email", email);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
    public class PaymentInfo
        {
            public string Name { get; set; }
            public string Course { get; set; }
            public string Amount { get; set; }
        }

    }

