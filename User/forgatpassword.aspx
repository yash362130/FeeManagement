<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgatpassword.aspx.cs" Inherits="Fee_Management.User.forgatpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" integrity="..." crossorigin="anonymous" />
    <link href="../CSS/login_style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="login-page">
            <div class="form">
                <div class="login">
                    <div class="login-header">
                        <h3>Forgate Password</h3>
                        <p>Please enter your Email to Password.</p>
                    </div>
                </div>
                <div class="login-form">
                    <div class="input-with-icon">
                        <i class="fa fa-envelope"></i>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Email" ControlToValidate="email" Display="Dynamic" ForeColor="Red" ValidationExpression="^\S+@\S+$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="email" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="email" runat="server" placeholder="Enter Your Email" CssClass="icon-input"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnotp" runat="server" CssClass="button" Style="background-color: #328f8a" OnClick="btnotp_Click" Text="Send Otp" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <div class="input-with-icon">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtopt" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtopt" runat="server" Visible="false" placeholder="Enter otp" CssClass="icon-input" MaxLength="6"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnverify" runat="server" Text="Verify Otp" CssClass="button" Style="background-color: #328f8a" OnClick="btnverify_Click" Visible="false" />
                    <a href="login.aspx">login</a><br />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
