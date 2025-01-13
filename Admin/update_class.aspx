<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="update_class.aspx.cs" Inherits="Fee_Management.Admin.update_class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/update_class.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="center-container">
                <div class="form-group">
                    <h1>Update Student Class</h1>
                    <br />
                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Current Class:"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="currentClass" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="currentClass" runat="server" CssClass="input-field" ClientIDMode="Static">
                        <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
                    </asp:DropDownList><br />

                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="New Class:"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="newClass" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="newClass" runat="server" CssClass="input-field" ClientIDMode="Static">
                        <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:Button ID="updateButton" runat="server" Text="Update Class" CssClass="submit-button" OnClick="updateButton_Click" />
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </div>
            </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var currentClassDropDown = document.getElementById("currentClass");
            var newClassDropDown = document.getElementById("newClass");

            currentClassDropDown.addEventListener("change", function () {
                var selectedValue = currentClassDropDown.value;

                var options = newClassDropDown.options;
                for (var i = 0; i < options.length; i++) {
                    if (options[i].value === selectedValue) {
                        options[i].style.display = "none";
                    } else {
                        options[i].style.display = "block";
                    }
                }
            });
        });

    </script>
</asp:Content>

