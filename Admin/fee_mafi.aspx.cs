using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.Admin
{
    public partial class fee_mafi : System.Web.UI.Page
    {
        string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;Trusted_connection=True;Encrypt=False";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmail"] != null)
                {
                    BindData();
                }
                else
                {
                    Response.Redirect("admin_login.aspx");
                }
            }
        }

        private void BindData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentsWithFeeWaiver", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Check if there is data to display
                        if (dt.Rows.Count > 0)
                        {
                            // Bind the DataTable to the Repeater control
                            Repeater1.DataSource = dt;
                            Repeater1.DataBind();
                        }
                        else
                        {
                            // Display a message or handle the case when no data is found
                            lblMessage.Text = "No records found.";
                        }
                    }
                }
            }
        }
        protected void btnPopup_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string email = btn.CommandArgument;

            // Call a method to update the fee_waiver status
            UpdateFeeWaiverStatus(email);

            // Rebind the data after updating
            BindData();

            Response.Redirect(Request.RawUrl);
        }
        private void UpdateFeeWaiverStatus(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Call the stored procedure instead of constructing the query
                using (SqlCommand cmd = new SqlCommand("UpdateFeeWaiverStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}