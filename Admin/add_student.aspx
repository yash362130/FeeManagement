<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="add_student.aspx.cs" Inherits="Fee_Management.Admin.add_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="../CSS/add_student_fee.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form">
        <h2>Student Registration Form</h2>
        <div class="form-group">
            <asp:Label ID="Name" CssClass="title" runat="server" Text="Student Name:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="studentName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="studentName" CssClass="form-control" runat="server" placeholder="Enter Your Full Name" />
        </div>
        <div class="form-group">
            <asp:Label ID="email_label" CssClass="title" runat="server" Text="Email:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="email" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="email" ErrorMessage="Enter a valid Email-address" ForeColor="Red" Display="Dynamic" ValidationExpression="^\S+@\S+$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="email" CssClass="form-control" runat="server" placeholder="Enter Your Email" OnBlur="checkEmailExists" TextMode="Email" AutoPostBack="True" OnTextChanged="email_TextChanged" />
        </div>
        <div class="form-group">
            <asp:Label ID="number" CssClass="title" runat="server" Text="Phone Number:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="phoneNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="phoneNumber" Display="Dynamic" ErrorMessage="Invalid mobile number format" ForeColor="Red" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="phoneNumber" CssClass="form-control" runat="server" placeholder="Enter Your Phone Number" MaxLength="10" onkeypress="return validatePhoneNumberInput(event);" />
        </div>
        <div class="form-group">
            <asp:Label ID="course" runat="server" CssClass="title" Text="Course Details:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="courseDetails" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="courseDetails" CssClass="form-control" runat="server">
                <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="Label4" CssClass="title" runat="server" Text="Address:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="address" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="address" CssClass="form-control" runat="server" placeholder="Enter Your Address" TextMode="MultiLine" Rows="4" />
        </div>
        <div class="form-group">
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Fee Waiver" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" /><br />
            <asp:TextBox ID="FeeWaiverTextBox" CssClass="form-control" runat="server" placeholder="Enter Fee Waiver Details" Visible="false" />
        </div>
        <br />
        <asp:Button ID="submitBtn" Text="Submit" CssClass="btn btn-primary" runat="server" OnClick="submitBtn_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>

    <script type="text/javascript">
        function validatePhoneNumberInput(event) {
            var charCode = event.which ? event.which : event.keyCode;

            if ((charCode >= 48 && charCode <= 57) || charCode === 8) {
                return true;
            } else {
                event.preventDefault();
                return false;
            }
        }

        function CheckBox1_CheckedChanged() {
            var checkBox = document.getElementById('<%= CheckBox1.ClientID %>');
            var textBox = document.getElementById('<%= FeeWaiverTextBox.ClientID %>');

            textBox.style.display = checkBox.checked ? 'block' : 'none';
        }

        document.addEventListener('DOMContentLoaded', function () {
            CheckBox1_CheckedChanged();
        });
    </script>
</asp:Content>
