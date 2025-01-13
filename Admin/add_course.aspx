<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="add_course.aspx.cs" Inherits="Fee_Management.Admin.add_course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/update_class.css" rel="stylesheet" />
    <div class="center-container">
        <div class="form-group">
            <asp:Label ID="lblcourse" runat="server" Text="Enter Course Name:"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="addcourse" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="addcourse" style="width:70%;" CssClass="input-field" runat="server" placeholder="Enter Course Name"></asp:TextBox>
            <asp:Button ID="add" CssClass="submit-button" runat="server" Text="Add" OnClick="add_Click" />
            <asp:Label ID="message" runat="server" Text="" Visible="false" ></asp:Label>
        </div>
    </div>
</asp:Content>
