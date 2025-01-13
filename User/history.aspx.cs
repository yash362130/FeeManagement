using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Fee_Management.Admin.fee_information;
using iText.Layout;
using iText.Layout.Borders;


namespace Fee_Management.User
{
    public partial class history : System.Web.UI.Page
    {
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=Fee Management;Integrated Security=True;";

        public Button sender { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Useremail"] != null)
                {
                    BindData();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        private void BindData()
        {
            string useremail = Session["Useremail"].ToString();
            string query = "SELECT fee_details.*, payment_information.creationdate " +
                           "FROM fee_details " +
                           "INNER JOIN payment_information ON fee_details.paymentid = payment_information.orderid " +
                           "WHERE fee_details.email = @Email AND fee_details.status = 'paid';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", useremail);

                    Repeater1.DataSource = command.ExecuteReader();
                    Repeater1.DataBind();
                }
            }
        }

        protected void download_Click(object sender, EventArgs e)
        {

             Button btnDownload = (Button)sender;

            // Find the parent repeater item
            RepeaterItem item = (RepeaterItem)btnDownload.NamingContainer;

            if (item != null)
            {
                // Get data from the repeater item
                Label lblName = (Label)item.FindControl("name");
                Label lblPaymentId = (Label)item.FindControl("paymentid");
                Label lblDescription = (Label)item.FindControl("description");
                Label lblAmount = (Label)item.FindControl("amount");
                Label lblCreationDate = (Label)item.FindControl("creationdate");
                Label lblCourse = (Label)item.FindControl("course");

                if (lblName != null && lblPaymentId != null && lblDescription != null && lblAmount != null && lblCreationDate != null && lblCourse != null)
                {
                    string studentName = lblName.Text;
                    string paymentId = lblPaymentId.Text;
                    string feeDescription = lblDescription.Text;
                    string amount = lblAmount.Text;
                    string creationDate = lblCreationDate.Text;
                    string course = lblCourse.Text;

                    // Create a PDF document
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfWriter writer = new PdfWriter(stream);
                        PdfDocument pdf = new PdfDocument(writer);
                        Document document = new Document(pdf);
                      
                        // Add header to the PDF
                        Paragraph header = new Paragraph("Shree Brahamanand Group Of Colleges")
                            .SetFontSize(24)
                            .SetFontColor(iText.Kernel.Colors.ColorConstants.ORANGE)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        document.Add(header);

                        // Add invoice content to the PDF with a table structure
                        iText.Layout.Element.Table table = new iText.Layout.Element.Table(2).UseAllAvailableWidth();

                        table.AddCell(new Cell().Add(new Paragraph("Student Name:")));
                        table.AddCell(new Cell().Add(new Paragraph(studentName)));

                        table.AddCell(new Cell().Add(new Paragraph("Payment Id:")));
                        table.AddCell(new Cell().Add(new Paragraph(paymentId)));

                        table.AddCell(new Cell().Add(new Paragraph("Fee Description:")));
                        table.AddCell(new Cell().Add(new Paragraph(feeDescription)));

                        table.AddCell(new Cell().Add(new Paragraph("Amount:")));
                        table.AddCell(new Cell().Add(new Paragraph(amount)));

                        table.AddCell(new Cell().Add(new Paragraph("Creation Date:")));
                        table.AddCell(new Cell().Add(new Paragraph(creationDate)));

                        table.AddCell(new Cell().Add(new Paragraph("Course:")));
                        table.AddCell(new Cell().Add(new Paragraph(course)));

                        document.Add(table);

                        Paragraph note = new Paragraph("Note: This is an online receipt. No signature is required. Collect hard copy from admin")
                        .SetFontSize(10)
                        .SetFontColor(iText.Kernel.Colors.ColorConstants.RED)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginTop(20); // Adjust margin top as needed

                        document.Add(note);

                        // Save the PDF document
                        document.Close();

                        // Send the PDF to the client for download
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", $"attachment;filename=Fee Invoice.pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(stream.ToArray());
                        Response.End();
                    }
                }
                else
                {
                    // Log or handle the case where one or more labels are null
                    Response.Write("One or more labels not found.");
                }
            }
            else
            {
                // Log or handle the case where the repeater item is null
                Response.Write("Repeater item not found.");
            }
        }
    }
}