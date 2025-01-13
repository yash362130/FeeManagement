using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ListItemWeb = System.Web.UI.WebControls.ListItem;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using System.Configuration;
using Razorpay.Api;
using System.Net.Mail;
using System.Net;


namespace Fee_Management.Admin
{
    public partial class fee_information : System.Web.UI.Page
    {
        
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=Fee Management;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Add this check to avoid reloading dropdown on postback
            {
                if (Session["AdminEmail"] != null)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand("SelectAllCourses", connection);
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            courseDropdown.Items.Clear(); // Clear the dropdown list before repopulating

                            courseDropdown.Items.Add(new System.Web.UI.WebControls.ListItem("Select Course", ""));

                            while (reader.Read())
                            {
                                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                                item.Text = reader["course"].ToString();
                                item.Value = reader["course"].ToString();

                                courseDropdown.Items.Add(item);
                            }

                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Page_Load: {ex.Message}");
                    }
                }
                else
                {
                    Response.Redirect("admin_login.aspx");
                }
            }
        }

        public void showcourse()
        {
            
        }
        protected void PopulateDescriptionDropdown(string selectedCourse)
        {
            string query = "PopulateDescriptionDropdown"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Course", selectedCourse);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Create a HashSet to store unique descriptions
                        HashSet<string> descriptions = new HashSet<string>();

                        // Add existing items to the HashSet to prevent duplicates
                        foreach (System.Web.UI.WebControls.ListItem existingItem in feeDescriptionDropdown.Items)
                        {
                            descriptions.Add(existingItem.Value);
                        }

                        // Clear the dropdown list before repopulating
                        feeDescriptionDropdown.Items.Clear();

                        while (reader.Read())
                        {
                            string description = reader["fee_description"].ToString();

                            // Check if the description is not already in the dropdown
                            if (!descriptions.Contains(description))
                            {
                                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                                item.Text = description;
                                item.Value = description;

                                feeDescriptionDropdown.Items.Add(item);

                                // Add the description to the HashSet
                                descriptions.Add(description);
                            }
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        protected void courseDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = courseDropdown.SelectedItem.Value;
            PopulateDescriptionDropdown(selectedCourse);
        }

        protected DataTable FetchDataFromDatabase(string selectedCourse, string selectedDescription)
        {
            string query = "FetchDataFromDatabase"; 

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SelectedCourse", selectedCourse);
                    command.Parameters.AddWithValue("@SelectedDescription", selectedDescription);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }


        protected void feeDescriptionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void feeinformation_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowView = e.Item.DataItem as DataRowView;


                if (rowView != null)
                {
                    string status = rowView["status"].ToString().ToLower();

                    Label statusLabel = e.Item.FindControl("status") as Label; // Assuming a label to display the symbol
                    Label noteLabel = e.Item.FindControl("note") as Label; // Assuming a label to display the symbol
                    if (statusLabel != null)
                    {
                        if (status == "paid")
                        {
                            // Set symbol for paid status
                            statusLabel.Text = "<span style='font-size: 24px;'>💵</span>";
                        }
                        else if (status == "pending")
                        {
                            // Set symbol for pending status
                            statusLabel.Text = "<span style='font-size: 24px;'>⏳</span>";
                        }
                        else
                        {
                            // Handle other statuses if needed
                            statusLabel.Text = ""; // Default case: no symbol
                        }
                    }

                    Button payButton = e.Item.FindControl("pay") as Button; // Replace "yourButtonID" with the ID of your button
                    Button deleteButton = e.Item.FindControl("delete") as Button; // Replace "yourButtonID" with the ID of your button

                    if (status == "paid" && payButton != null)
                    {
                        payButton.Visible = false;
                        deleteButton.Visible = false;
                        noteLabel.Visible = true;
                        noteLabel.Text = "No action";
                    }

                    if (payButton != null)
                    {
                        payButton.CommandArgument = rowView["id"].ToString(); // Assuming "id" is the unique identifier in your table
                        payButton.Click += pay_Click;
                    }

                    if (deleteButton != null)
                    {
                        deleteButton.CommandArgument = rowView["id"].ToString(); // Assuming "id" is the unique identifier in your table
                        deleteButton.Click += delete_Click;
                    }
                }
                Button btnSavePayment = e.Item.FindControl("btnSavePayment") as Button;
                if (btnSavePayment != null)
                {
                    btnSavePayment.CommandArgument = e.Item.ItemIndex.ToString(); // Set the CommandArgument to the item index
                    btnSavePayment.Click += btnSavePayment_Click1; // Attach the event handler
                }
            }
        }

        public void DeleteRecord(int id)
        {
            string query = "DeleteRecord"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Assuming you're using ASP.NET WebForms, registering script to show alert
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowInfoMessage", "alert('Record Deleted Successfully.');", true);
                }
            }
        }


        // private string clickedID;
        protected void pay_Click(object sender, EventArgs e)
        {
            Button payButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)payButton.NamingContainer;

            // Access the controls inside the RepeaterItem to get the id
            Label lblID = (Label)item.FindControl("id");
            Label lblAmount = (Label)item.FindControl("amount");
            Label lblEmail=(Label)item.FindControl("email");    

            Session["ClickedID"] = lblID.Text;
            Session["PaymentAmount"] = lblAmount.Text;
            Session["Email"]=lblEmail.Text;

            // Call the JavaScript function to show the pay modal
            ScriptManager.RegisterStartupScript(this, GetType(), "showPayModal", "showPayModal();", true);
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            string ID = deleteButton.CommandArgument; // Get the ID of the item to update

            // Convert the ID to an integer
            if (int.TryParse(ID, out int recordID))
            {
                // Update the status from pending to paid in the database
                DeleteRecord(recordID);

                // Re-bind the data after updating the status
                string selectedCourse = courseDropdown.SelectedItem.Value;
                string selectedDescription = feeDescriptionDropdown.SelectedItem.Value;

                // Fetch data from fee_details table based on selectedCourse and selectedDescription
                DataTable table = FetchDataFromDatabase(selectedCourse, selectedDescription);

                if (table != null && table.Rows.Count > 0)
                {
                    feeinformation.DataSource = table;
                    feeinformation.DataBind();
                }
            }
            else
            {
                // Handle the case where ID conversion to int fails
                // Provide an error message or handle the scenario accordingly
            }
        }

        protected void showdata_Click(object sender, EventArgs e)
        {
            string selectedCourse = courseDropdown.SelectedItem.Value;
            string selectedDescription = feeDescriptionDropdown.SelectedItem.Value;

            // Fetch data from fee_details table based on selectedCourse and selectedDescription
            DataTable table = FetchDataFromDatabase(selectedCourse, selectedDescription);

            if (table != null && table.Rows.Count > 0)
            {
                feeinformation.DataSource = table;
                feeinformation.DataBind();
            }
        }

        protected void downloadBtn_Click(object sender, EventArgs e)
        {
            string selectedCourse = courseDropdown.SelectedItem.Value;
            string selectedFeeDescription = feeDescriptionDropdown.SelectedItem.Value;

            // Generate PDF content using a library like iTextSharp
            byte[] pdfBytes = GeneratePDF(selectedCourse, selectedFeeDescription);

            // Send the PDF as a download to the user
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename='" + selectedCourse + "'-'" + selectedFeeDescription + "'.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }
        public class FeeInformation
        {
            // Add properties based on your data structure
            public string Property1 { get; set; }
            public string Property2 { get; set; }
            public string Property3 { get; internal set; }
            public string Property4 { get; internal set; }
            public string Property5 { get; internal set; }
            public bool FeeWaiver { get; set; }

            // Add more properties as needed
        }
        private List<FeeInformation> GetFeeInformation(string selectedCourse, string selectedFeeDescription)
        {
            List<FeeInformation> feeInformationList = new List<FeeInformation>();

            string query = "GetFeeInformation"; // Stored procedure name

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SelectedCourse", selectedCourse);
                    command.Parameters.AddWithValue("@SelectedFeeDescription", selectedFeeDescription);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FeeInformation feeInfo = new FeeInformation
                        {
                            // Populate properties based on your fee_details table columns
                            Property1 = reader["student_name"].ToString(),
                            Property2 = reader["phone_number"].ToString(),
                            Property3 = reader["amount"].ToString(),
                            Property4 = reader["course"].ToString(),
                            Property5 = reader["status"].ToString(),
                            // Add more properties as needed
                        };

                        feeInformationList.Add(feeInfo);
                    }

                    reader.Close();
                }
            }

            return feeInformationList;
        }

        private byte[] GeneratePDF(string selectedCourse, string selectedFeeDescription)
        {
            // Add your logic to retrieve fee information based on selectedCourse and selectedFeeDescription
            List<FeeInformation> feeInformationList = GetFeeInformation(selectedCourse, selectedFeeDescription);

            // Use iTextSharp and Bouncy Castle to create the PDF content
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a new document
                PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));

                // Create a document
                Document document = new Document(pdfDocument);

                // Set border and background color for the page
                PdfCanvas canvas = new PdfCanvas(pdfDocument.AddNewPage());
                canvas.SetLineWidth(3f);  // Set border width
                canvas.Rectangle(30, 30, pdfDocument.GetDefaultPageSize().GetWidth() - 60, pdfDocument.GetDefaultPageSize().GetHeight() - 60);
                canvas.SetStrokeColor(iText.Kernel.Colors.ColorConstants.BLACK); // Set border color
                canvas.Stroke();
                canvas.SetFillColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY); // Set background color
                canvas.Fill();

                // Add header with the title "Shree Brahmanand Group Collages" in orange color
                Paragraph header = new Paragraph("Shree Brahmanand Group Collages")
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.ORANGE)
                    .SetBold()
                    .SetFontSize(25) // Set font size for the header
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER); // Center align the header
                document.Add(header);

                // Add some space after the header
                document.Add(new Paragraph().SetMarginBottom(20));

                // Fetch additional details from fee_details based on selectedCourse and selectedFeeDescription
                string detailsQuery = "SELECT * FROM fee_details WHERE course = @SelectedCourse AND fee_description = @SelectedFeeDescription";
                using (SqlConnection detailsConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand detailsCommand = new SqlCommand(detailsQuery, detailsConnection))
                    {
                        detailsCommand.Parameters.AddWithValue("@SelectedCourse", selectedCourse);
                        detailsCommand.Parameters.AddWithValue("@SelectedFeeDescription", selectedFeeDescription);

                        detailsConnection.Open();
                        SqlDataReader detailsReader = detailsCommand.ExecuteReader();

                        if (detailsReader.Read())
                        {
                            // Add title to the document
                            string courseTitle = detailsReader["course"].ToString();
                            string courseType = detailsReader["fee_description"].ToString();
                            document.Add(new Paragraph($"Title: {courseTitle} ({courseType})")
                                .SetBold() // Set font to bold
                                .SetFontSize(14) // Set font size for the title
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            );

                            // Add additional details to the document
                            string declareDate = detailsReader["declare_date"].ToString();
                            string lastDate = detailsReader["last_date"].ToString();
                            document.Add(new Paragraph($"Declare Date: {declareDate}")
                                .SetBold() // Set font to bold
                                .SetFontSize(12) // Set font size for the details
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            );
                            document.Add(new Paragraph($"Last Date: {lastDate}")
                                .SetBold() // Set font to bold
                                .SetFontSize(12) // Set font size for the details
                                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            );

                            // Add an empty line for better formatting
                            document.Add(new Paragraph());
                        }

                        detailsReader.Close();
                    }
                }

                // Create a table with five columns (including the new title column)
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(5);

                // Add header row
                table.AddHeaderCell("Student Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                table.AddHeaderCell("Phone Number").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                table.AddHeaderCell("Amount").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                table.AddHeaderCell("Course").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                table.AddHeaderCell("Status").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                // Add content to the table based on feeInformationList
                foreach (FeeInformation feeInfo in feeInformationList)
                {
                    // Set font color to red if the status is pending
                    if (feeInfo.Property5.ToLower() == "pending")
                    {
                        table.AddCell(new Paragraph(feeInfo.Property1).SetBold().SetFontSize(12).SetFontColor(iText.Kernel.Colors.ColorConstants.RED)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property2).SetBold().SetFontSize(12).SetFontColor(iText.Kernel.Colors.ColorConstants.RED)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property3).SetBold().SetFontSize(12).SetFontColor(iText.Kernel.Colors.ColorConstants.RED)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property4).SetBold().SetFontSize(12).SetFontColor(iText.Kernel.Colors.ColorConstants.RED)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property5).SetBold().SetFontSize(12).SetFontColor(iText.Kernel.Colors.ColorConstants.RED)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    }
                    else
                    {
                        table.AddCell(new Paragraph(feeInfo.Property1).SetBold().SetFontSize(12)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property2).SetBold().SetFontSize(12)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property3).SetBold().SetFontSize(12)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property4).SetBold().SetFontSize(12)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        table.AddCell(new Paragraph(feeInfo.Property5).SetBold().SetFontSize(12)).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    }
                } 

                // Add the table to the document
                document.Add(table).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

                // Close the document
                document.Close();

                // Return the generated PDF content as a byte array
                return memoryStream.ToArray();
            }
        }
               
        protected void btnSavePayment_Click1(object sender, EventArgs e)
        {
            try
            {
                // Get values
                string paymentReference = txtPayAmount.Text.Trim();

                // Retrieve the clicked ID and amount from session variables
                string clickedID = Session["ClickedID"] as string;
                string paymentAmount = Session["PaymentAmount"] as string;
                string email = Session["email"] as string;

                if (!string.IsNullOrEmpty(clickedID) && int.TryParse(clickedID, out int studentId))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Assuming fee_details is your table name
                        string updateQuery = "UPDATE fee_details SET paymentid = @PaymentId, status = 'paid' WHERE id = @StudentId";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.Add("@PaymentId", SqlDbType.VarChar).Value = paymentReference;
                            command.Parameters.Add("@StudentId", SqlDbType.Int).Value = studentId;

                            int rowsAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"Rows affected: {rowsAffected}");

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine($"Update successful for Student ID: {studentId}");

                                // Insert into payment_information table
                                InsertIntoPaymentInformation(paymentReference, paymentAmount);

                                // Send payment confirmation email
                                SendPaymentConfirmationEmail(email, int.Parse(paymentAmount));

                                // Display success message (you can customize this)
                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowSuccessMessage", "alert('Payment information updated successfully.');", true);

                                txtPayAmount.Text = string.Empty;

                                // Close the modal after saving
                                ScriptManager.RegisterStartupScript(this, GetType(), "CloseModalScript", "closePayModal();", true);

                                // Re-bind the data after updating the payment details
                                string selectedCourse = courseDropdown.SelectedItem.Value;
                                string selectedDescription = feeDescriptionDropdown.SelectedItem.Value;

                                // Fetch data from fee_details table based on selectedCourse and selectedDescription
                                DataTable table = FetchDataFromDatabase(selectedCourse, selectedDescription);

                                if (table != null && table.Rows.Count > 0)
                                {
                                    feeinformation.DataSource = table;
                                    feeinformation.DataBind();
                                }
                            }
                            else
                            {
                                // Display a message indicating that no rows were updated
                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowInfoMessage", "alert('No rows were updated.');", true);
                            }
                        }
                    }
                }
                else
                {
                    // Display a more informative error message
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowErrorMessage", "alert('Error: Please provide a valid Student ID.');", true);
                }
            }
            catch (Exception ex)
            {
                // Log the error for troubleshooting
                Console.WriteLine($"Error in btnSavePayment_Click1: {ex.Message}");

                // Display a detailed error message to the user
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowErrorMessage", $"alert('An error occurred: {ex.Message}. Please try again or contact support.');", true);
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
        private void InsertIntoPaymentInformation(string orderId, string amount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming payment_information is your table name
                string insertQuery = "INSERT INTO payment_information (orderid, amount, status, creationdate) VALUES (@OrderId, @Amount, 'paid', GETDATE())";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = orderId;
                    command.Parameters.Add("@Amount", SqlDbType.VarChar).Value = amount;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}