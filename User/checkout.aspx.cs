using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fee_Management.User
{
    public partial class checkout : System.Web.UI.Page
    {
        public string name;
        public string amount;
        public string email;
        public string course;
        public string orderId;
        public string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Useremail"] != null)
            {
                amount = (Convert.ToInt32(Request.QueryString["Amount"]) * 100).ToString();
                course = Request.QueryString["Course"].ToString();
                name = Request.QueryString["Name"].ToString();
                email = Request.QueryString["Email"].ToString();
                id = Request.QueryString["ID"].ToString();
                Session["studentEmail"] = email;
                Session["pid"] = id;

                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", amount);
                input.Add("currency", "INR");
                input.Add("payment_capture", 1);

                string key = "rzp_test_9ZgzBU3MVBh8jC";
                string secret = "eZceASuUFChBT3XaYQibLerI";

                RazorpayClient client = new RazorpayClient(key, secret);

                Razorpay.Api.Order order = client.Order.Create(input);
                orderId = order["id"].ToString();
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}