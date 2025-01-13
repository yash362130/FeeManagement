<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="Fee_Management.User.checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="callback.aspx" method="post" runat="server">
        <script src="https://checkout.razorpay.com/v1/checkout.js"
            data-key="rzp_test_9ZgzBU3MVBh8jC"
            data-amount="<%=amount%>"
            data-name="<%=name%>"
            data-order_id="<%=orderId%>"
            data-image="https://razorpay.com/favicon.png"
            data-prefill.name="<%=name%>"
            data-prefill.email="<%=email%>"
            data-theme.color="#F37254">
        </script>
    </form>
    <script>
        window.addEventListener('load', function () {
            // Simulate a click on the Razorpay checkout button after the page loads
            var checkoutButton = document.querySelector('.razorpay-payment-button');
            if (checkoutButton) {
                checkoutButton.click();
            }
        });
    </script>
</body>
</html>
