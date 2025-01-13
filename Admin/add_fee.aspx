<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="add_fee.aspx.cs" Inherits="Fee_Management.Admin.add_fee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/add_student_fee.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form">
        <h1>Manage Fee</h1>
        <div class="form-group">
            <asp:Label ID="lblFeeDescription" runat="server" Text="Fee Description"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFeeDescription" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFeeDescription" runat="server" CssClass="form-control" placeholder="Enter Fee Description"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="Enter Fee Amount"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="CourseDetails" runat="server" Text="Course Details"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="currentClass" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="currentClass" runat="server" CssClass="form-control" ClientIDMode="Static">
                <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Label ID="lblCurrentDate" runat="server" Text="Declare Date"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCurrentDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblLastDate" runat="server" Text="Last Date"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtCurrentDate" ControlToValidate="txtLastDate" Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" placeholder="Select Last Date"></asp:TextBox>
                    <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>
                    <br />
                    <br />
                    <div class="form-group">
                        <asp:CheckBox ID="chkNoDiscount" runat="server" Text="No Discount for Fee Waiver Students" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:Button ID="btnAddFee" runat="server" Text="Add Fee" CssClass="btn btn-primary" OnClick="btnAddFee_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
