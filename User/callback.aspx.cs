using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace Fee_Management.User
{
    public partial class callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["studentEmail"] != null)
            {
                try
                {
                    string paymentId = Request.Form["razorpay_payment_id"];
                    string orderId = Request.Form["razorpay_order_id"];
                    string signature = Request.Form["razorpay_signature"];

                    string key = "rzp_test_9ZgzBU3MVBh8jC";
                    string secret = "eZceASuUFChBT3XaYQibLerI";

                    RazorpayClient client = new RazorpayClient(key, secret);

                    // Fetch order details to get the amount
                    Order order = client.Order.Fetch(orderId);
                    int amount = order.Attributes["amount"];
                    string studentEmail = Session["studentEmail"] as string;
                    string pid = Session["pid"] as string;

                    amount /= 100;

                    Dictionary<string, string> attributes = new Dictionary<string, string>();
                    attributes.Add("razorpay_payment_id", paymentId);
                    attributes.Add("razorpay_order_id", orderId);
                    attributes.Add("razorpay_signature", signature);
                    Utils.verifyPaymentSignature(attributes);

                    string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;Trusted_connection=True;Encrypt=False";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string insertQuery = "INSERT INTO payment_information (orderid, amount, status, creationdate) VALUES (@PaymentId, @Amount, @Status, @PaymentDate)";
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@PaymentId", paymentId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@Status", "paid");
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Insert payment ID into fee_details table where pid matches
                        string insertPaymentIdQuery = "UPDATE fee_details SET paymentid = @PaymentId WHERE id = @Pid";
                        SqlCommand paymentIdCommand = new SqlCommand(insertPaymentIdQuery, connection);
                        paymentIdCommand.Parameters.AddWithValue("@PaymentId", paymentId);
                        paymentIdCommand.Parameters.AddWithValue("@Pid", pid);

                        paymentIdCommand.ExecuteNonQuery();

                        string updateStatusQuery = @"
                                        UPDATE fee_details
                                        SET status = 'paid'
                                        WHERE id = @Pid 
                                        AND paymentid = (SELECT orderid FROM payment_information WHERE orderid = @PaymentId) ";

                        SqlCommand updateStatusCommand = new SqlCommand(updateStatusQuery, connection);
                        updateStatusCommand.Parameters.AddWithValue("@Pid", pid);
                        updateStatusCommand.Parameters.AddWithValue("@PaymentId", paymentId);

                        int rowsAffected = updateStatusCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Update was successful
                            // Perform any additional actions if needed
                        }
                    }

                    DPaymentid.Text = "Payment Id: "+paymentId;
                    pAmount.Text = "Amount : "+amount.ToString();
                    email.Text = "Email : "+studentEmail;

                    h1Message.InnerText = "Transaction Successful";

                    SendPaymentConfirmationEmail(studentEmail, amount);
                }
                catch (Exception ex)
                {
                    email.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect("Errorpage.aspx");
            }
        }
        private void SendPaymentConfirmationEmail(string userEmail, int paidAmount)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mahetayash8@gmail.com");
                mail.To.Add(userEmail);
                mail.Subject = "Fee Payment Confirmation";
                mail.Body = $"Dear User, \n\nYour fee payment of {paidAmount} has been successfully processed. Thank you for your payment.";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("mahetayash8@gmail.com", "twsl vkzh xwvz wosh");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                Response.Redirect(smtpEx.Message);
            }
            catch (Exception ex)
            {
                Response.Redirect($"{ex.Message}");
            }
        }
    }
}