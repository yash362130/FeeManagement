<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_login.aspx.cs" Inherits="Fee_Management.Admin.admin_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" integrity="..." crossorigin="anonymous" />
    <link href="../CSS/login_style.css" rel="stylesheet" />
     <script>
     function togglePasswordVisibility() {
         var passwordField = document.getElementById('password');
         var eyeIcon = document.getElementById('eye');

         if (passwordField.type === 'password') {
             passwordField.type = 'text';
             eyeIcon.classList.remove('fa-eye');
             eyeIcon.classList.add('fa-eye-slash');
         } else {
             passwordField.type = 'password';
             eyeIcon.classList.remove('fa-eye-slash');
             eyeIcon.classList.add('fa-eye');
         }
     }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <div class="login-page">
     <div class="form">
         <div class="login">
             <div class="login-header">
                 <h3>ADMIN LOGIN</h3>
                 <p>Please enter your credentials to login.</p>
             </div>
         </div>
         <div class="login-form">
             <div class="input-with-icon">
                 <i class="fa fa-envelope"></i>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid Email" ControlToValidate="email" Display="Dynamic" ForeColor="Red" ValidationExpression="^\S+@\S+$"></asp:RegularExpressionValidator>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="email" ForeColor="Red"></asp:RequiredFieldValidator>
                 <asp:TextBox ID="email" runat="server" placeholder="Email" CssClass="icon-input"></asp:TextBox>
             </div>
             <div class="input-with-icon">
                 <i class="fa fa-lock"></i>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="password" ForeColor="Red"></asp:RequiredFieldValidator>
                 <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Password" CssClass="icon-input"></asp:TextBox>
                 <span class="fa fa-eye" onclick="togglePasswordVisibility()"></span>
             </div>
             <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="button" Style="background-color: #328f8a" OnClick="LoginButton_Click" />
             <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
             <%--<a href="#">Forget Password</a>--%>
         </div>
     </div>
 </div>
    </form>
</body>
</html>
