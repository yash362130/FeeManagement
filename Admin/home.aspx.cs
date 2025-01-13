using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Fee_Management.Admin
{
    public partial class home : System.Web.UI.Page
    {
        public string PaymentData { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminEmail"] != null)
                {
                    if (Session["SelectedView"] != null)
                    {
                        string selectedView = Session["SelectedView"].ToString();
                        DisplayChart("section", selectedView);
                    }
                    else
                    {
                        DisplayChart("section", "daily");
                    }
                    DisplayCourseStudentCount();
                    DisplayOrderInformation();
                }
                else
                {
                    HttpContext.Current.Response.Redirect("admin_login.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void DisplayCourseStudentCount()
        {
            using (var connection = new SqlConnection("Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;Trusted_connection=True;Encrypt=False"))
            {
                connection.Open();
                using (var command = new SqlCommand("DisplayCourseStudentCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        if (dataTable.Rows.Count > 0)
                        {
                            Repeater1.DataSource = dataTable;
                            Repeater1.DataBind();
                        }
                        else
                        {
                            // Handle no data case
                        }
                    }
                }
            }
        }

        private void DisplayChart(string section, string displayOption)
        {
            string sqlQuery = string.Empty;

            switch (displayOption)
            {
                case "daily":
                    sqlQuery = @"
                SELECT 
                    CAST(creationdate AS DATE) AS created_at_date,
                    SUM(CASE WHEN status = 'paid' THEN 1 ELSE 0 END) AS successful_payments,
                    SUM(CASE WHEN status = 'unpaid' THEN 1 ELSE 0 END) AS unsuccessful_payments
                FROM payment_information
                WHERE DATEDIFF(DAY, creationdate, GETDATE()) = 0
                GROUP BY CAST(creationdate AS DATE)
                ORDER BY created_at_date;";
                    break;

                case "weekly":
                    sqlQuery = @"
                SELECT 
                    CAST(creationdate AS DATE) AS created_at_date,
                    SUM(CASE WHEN status = 'paid' THEN 1 ELSE 0 END) AS successful_payments,
                    SUM(CASE WHEN status = 'unpaid' THEN 1 ELSE 0 END) AS unsuccessful_payments
                FROM payment_information
                WHERE DATEDIFF(WEEK, creationdate, GETDATE()) = 0
                GROUP BY CAST(creationdate AS DATE)
                ORDER BY created_at_date;";
                    break;

                case "monthly":
                    sqlQuery = @"
                SELECT 
                    DATEADD(MONTH, DATEDIFF(MONTH, 0, creationdate), 0) AS created_at_date,
                    SUM(CASE WHEN status = 'paid' THEN 1 ELSE 0 END) AS successful_payments,
                    SUM(CASE WHEN status = 'unpaid' THEN 1 ELSE 0 END) AS unsuccessful_payments
                FROM payment_information
                GROUP BY DATEADD(MONTH, DATEDIFF(MONTH, 0, creationdate), 0)
                ORDER BY created_at_date;";
                    break;

                case "yearly":
                    sqlQuery = @"
                SELECT 
                    DATEPART(YEAR, creationdate) AS created_at_year,
                    SUM(CASE WHEN status = 'paid' THEN 1 ELSE 0 END) AS successful_payments,
                    SUM(CASE WHEN status = 'unpaid' THEN 1 ELSE 0 END) AS unsuccessful_payments
                FROM payment_information
                GROUP BY DATEPART(YEAR, creationdate)
                ORDER BY created_at_year;";
                    break;

                default:
                    // Handle unsupported displayOption or any other logic
                    break;
            }

            using (var connection = new SqlConnection("Data Source=YASH\\SQLEXPRESS;Initial Catalog=Fee Management;Integrated Security=True;Trusted_connection=True;Encrypt=False"))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    List<string> labels = new List<string>();
                    List<int> successfulPayments = new List<int>();
                    List<int> unsuccessfulPayments = new List<int>();

                    bool hasData = false;

                    while (reader.Read())
                    {
                        switch (displayOption)
                        {
                            case "yearly":
                                labels.Add(reader["created_at_year"].ToString());
                                break;
                            default:
                                labels.Add(reader["created_at_date"].ToString());
                                break;
                        }

                        successfulPayments.Add(Convert.ToInt32(reader["successful_payments"]));
                        unsuccessfulPayments.Add(Convert.ToInt32(reader["unsuccessful_payments"]));
                        hasData = true;
                    }

                    if (!hasData && displayOption == "daily")
                    {
                        // If no data is found for the daily section, display a message
                        string noDataMessage = "<div style='text-align:center; font-size: 24px;'>No Data Found Today</div>";
                        jsonContainer.InnerHtml = $"<div class='chart-container'>" + noDataMessage + "</div>";
                    }
                    else if(!hasData && displayOption == "weekly")   
                    {
                        string noDataMessage = "<div style='text-align:center; font-size: 24px;'>No Data Found in Week</div>";
                        jsonContainer.InnerHtml = $"<div class='chart-container'>" + noDataMessage + "</div>";
                    }
                    else if (!hasData && displayOption == "monthly") 
                    {
                        string noDataMessage = "<div style='text-align:center; font-size: 24px;'>No Data Found in Week</div>";
                        jsonContainer.InnerHtml = $"<div class='chart-container'>" + noDataMessage + "</div>";
                    }
                     else if (!hasData && displayOption == "yearly") 
                    {
                        string noDataMessage = "<div style='text-align:center; font-size: 24px;'>No Data Found in Week</div>";
                        jsonContainer.InnerHtml = $"<div class='chart-container'>" + noDataMessage + "</div>";
                    }
                    else
                    {

                        // Generate pie chart JavaScript code
                        string labelsArray = JsonConvert.SerializeObject(labels.ToArray());
                        string successfulPaymentsArray = JsonConvert.SerializeObject(successfulPayments.ToArray());
                        string unsuccessfulPaymentsArray = JsonConvert.SerializeObject(unsuccessfulPayments.ToArray());

                        Random random = new Random();

                        string chartScript = $@"
                        <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
                        <div style='width: 400px; height: 300px; margin: 20px auto;'>
                            <canvas id='myChart'></canvas>
                        </div>
                        <script>
                            var ctx = document.getElementById('myChart').getContext('2d');
                            var labels = {labelsArray};
                            var successfulPayments = {successfulPaymentsArray};
                            var unsuccessfulPayments = {unsuccessfulPaymentsArray};

                            var data = {{
                                labels: labels,
                                datasets: [
                                    {{
                                        label: 'Successful Payments',
                                        data: successfulPayments,
                                        backgroundColor: [
                                            'rgba(76, 175, 80, 0.5)', 
                                            'rgba(255, 193, 7, 0.5)', 
                                            'rgba(200, 100, 10, 0.5)', 
                                            'rgba(220, 163, 3, 0.5)', 
                                            'rgba(190, 80, 10, 0.5)', 
                                            'rgba(90, 180, 11, 0.5)', 
                                            'rgba(210, 10, 12, 0.5)', 
                                            // Add more colors as needed...
                                        ],
                                        borderColor: [
                                            'rgba(76, 175, 80, 0.5)', 
                                            'rgba(255, 193, 7, 0.5)', 
                                            'rgba(200, 100, 10, 0.5)', 
                                            'rgba(220, 163, 3, 0.5)', 
                                            'rgba(190, 80, 10, 0.5)', 
                                            'rgba(90, 180, 11, 0.5)', 
                                            'rgba(210, 10, 12, 0.5)', 
                                            // Corresponding border colors for each section...
                                        ],
                                        borderWidth: 1
                                    }},
                                    {{
                                        label: 'Unsuccessful Payments',
                                        data: unsuccessfulPayments,
                                        backgroundColor: [
                                            'rgba(244, 67, 54, 0.5)', // Red
                                            'rgba(121, 85, 72, 0.5)', // Brown
                                            // Add more colors as needed...
                                        ],
                                        borderColor: [
                                            'rgba(244, 67, 54, 1)',
                                            'rgba(121, 85, 72, 1)',
                                            // Corresponding border colors for each section...
                                        ],
                                        borderWidth: 1
                                    }}
                                ]
                            }};

                            var myChart = new Chart(ctx, {{
                                type: 'pie',
                                data: data,
                                options: {{
                                    responsive: true,
                                    maintainAspectRatio: false,
                                    tooltips: {{
                                        callbacks: {{
                                            label: function(tooltipItem, data) {{
                                                var dataset = data.datasets[tooltipItem.datasetIndex];
                                                var currentValue = dataset.data[tooltipItem.index];
                                                return 'Amount: ' + currentValue;
                                            }}
                                        }}
                                    }}
                                }}
                            }});
                        </script>";

                        jsonContainer.InnerHtml = $"<div class='chart-container'>" + chartScript + "</div>";

                    }
                }
            }
        }
        private void DisplayOrderInformation()
        {
            string connectionString = "Data Source=YASH\\SQLEXPRESS;Initial Catalog=\"Fee Management\";Integrated Security=True;Trusted_connection=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DisplayOrderInformation"; // Stored procedure name

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    connection.Close();

                    if (dataTable.Rows.Count > 0)
                    {
                        dataTable.DefaultView.Sort = "creationdate DESC";
                        DataTable sortedDataTable = dataTable.DefaultView.ToTable();

                        paymentinfo.DataSource = sortedDataTable;
                        paymentinfo.DataBind();
                    }
                    else
                    {
                        // Handle case where no records are found
                    }
                }
            }
        }
        
        protected void Daily_Click(object sender, EventArgs e)
        {
            Session["SelectedView"] = "daily";
            Response.Redirect(Request.RawUrl);
        }

        protected void Weekly_Click(object sender, EventArgs e)
        {
            Session["SelectedView"] = "weekly";
            Response.Redirect(Request.RawUrl);
        }

        protected void Monthly_Click(object sender, EventArgs e)
        {
            Session["SelectedView"] = "monthly";
            Response.Redirect(Request.RawUrl);
        }

        protected void year_Click(object sender, EventArgs e)
        {
            Session["SelectedView"] = "yearly";
            Response.Redirect(Request.RawUrl);
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("admin_login.aspx");
        }
    }
}
