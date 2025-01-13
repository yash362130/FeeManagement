<%@ Page Title="" Language="C#" MasterPageFile="~/userside.Master" AutoEventWireup="true" CodeBehind="user_information.aspx.cs" Inherits="Fee_Management.User.user_information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #328f8a;
            background-image: linear-gradient(45deg, #328f8a, #08ac4b);
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 1200px;
            margin: 20px auto;
        }

        .panel, .repeater-table {
            max-width: 100%;
            border-radius: 25px;
            margin: 20px;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .repeater-table {
            border-collapse: collapse;
            margin-top: 20px;
        }

        .repeater-row {
            border-bottom: 1px solid #ccc;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px;
        }

        .repeater-cell {
            flex: 1;
        }

            .repeater-cell:last-child {
                border-right: none;
            }

        .label-container {
            margin-bottom: 20px;
        }

        .label {
            font-weight: bold;
            color: #333;
        }

        .data-label {
            margin-right: 10px;
        }

        .bn632-hover {
            width: 100px;
            font-size: 16px;
            font-weight: 600;
            color: #fff;
            cursor: pointer;
            height: 45px;
            text-align: center;
            border: none;
            background-size: 300% 100%;
            border-radius: 50px;
            transition: background-position 0.4s ease-in-out, box-shadow 0.4s ease-in-out;
        }

            .bn632-hover:hover {
                background-position: 100% 0;
                box-shadow: 0 4px 15px 0 rgba(65, 132, 234, 0.75);
            }

            .bn632-hover:focus {
                outline: none;
            }

            .bn632-hover.bn26 {
                background-image: linear-gradient(to right, #25aae1, #4481eb, #04befe, #3f86ed);
            }

        @media screen and (max-width: 768px) {
            .panel, .repeater-table {
                padding: 15px;
                margin: 10px auto;
                width: 90%;
            }

            .repeater-row {
                flex-direction: column;
            }

            .label-container {
                margin-bottom: 10px;
            }

            .repeater-cell {
                flex: 100%;
            }
        }

        @media screen and (max-width: 480px) {
            .panel, .repeater-table {
                padding: 10px;
            }
        }

        .data-label {
            margin-inline: 0;
        }

        .edit-button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
        }

            .edit-button:hover {
                background-color: #45a049; /* Darker Green */
            }

        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.7);
        }

        .modal-content {
            background-color: #fefefe;
            margin: 10% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 60%;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .modal-header {
            padding: 10px;
            border-bottom: 1px solid #ddd;
            text-align: center;
        }

            .modal-header h2 {
                margin: 0;
                font-size: 1.5em;
            }

        .modal-body {
            padding: 20px;
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
            cursor: pointer;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        .custom {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            font-size: 16px;
        }

            .custom:focus {
                outline: none;
                border-color: DodgerBlue;
                box-shadow: 0 0 8px rgba(0, 0, 255, 0.4);
            }

        .custombutton {
            padding: 10px 15px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

            .custombutton:hover {
                background-color: #45a049;
            }

            .custombutton:focus {
                outline: none;
            }
    </style>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showPayModal() {
            // Use jQuery to show the modal
            $('#EditEmailModal').modal('show');
        }

        function closePayModal() {
            // Use jQuery to hide the modal
            $('#EditEmailModal').modal('hide');
            console.log("Modal closed");
        }

        window.onclick = function (event) {
            var modal = document.getElementById('EditEmailModal');
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        };
        function showPayModalpassword() {
            // Use jQuery to show the modal
            $('#EditpasswordModal').modal('show');
        }

        function closePayModalpassword() {
            // Use jQuery to hide the modal
            $('#EditpasswordModal').modal('hide');
            console.log("Modal closed");
        }

        window.onclick = function (event) {
            var modal = document.getElementById('EditpasswordModal');
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="panel">
        <div class="label-container">
            <asp:Label ID="Label1" CssClass="label" runat="server" Text="Name:"></asp:Label>
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </div>
        <div class="label-container">
            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Email:"></asp:Label>
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </div>
        <div class="label-container">
            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Course:"></asp:Label>
            <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label>
        </div>
        <div class="label-container">
            <asp:Label ID="Label4" runat="server" CssClass="label" Text="Phone Number:"></asp:Label>
            <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label>
        </div>
        <div class="label-container">
            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Address:"></asp:Label>
            <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label>
        </div>
        <div class="label-container">
            <asp:Button ID="btnEditPassword" runat="server" CssClass="edit-button" Text="Edit Password" OnClick="btnEditPassword_Click" />
            <asp:Button ID="btnEditEmail" runat="server" CssClass="edit-button" Text="Edit Email" OnClick="btnEditEmail_Click" />
        </div>
    </div>
    <div class="repeater-table">
        <asp:Repeater ID="FeeReapter" runat="server" OnItemDataBound="FeeReapter_ItemDataBound">
            <HeaderTemplate>
                <div class="repeater-row">
                    <div class="repeater-cell">
                        <span cssclass="data-label" style="margin-left: 100px; font-weight: bold;">Fee Description</span>
                    </div>
                    <div class="repeater-cell">
                        <span cssclass="data-label" style="margin-left: 130px; font-weight: bold;">Amount</span>
                    </div>
                    <div class="repeater-cell">
                        <span cssclass="data-label" style="margin-left: 90px; font-weight: bold;">Declare Date</span>
                    </div>
                    <div class="repeater-cell">
                        <span cssclass="data-label" style="margin-left: 50px; font-weight: bold;">Last Date</span>
                    </div>
                    <div class="repeater-cell">
                        <span cssclass="data-label" style="margin-right: 310px; font-weight: bold;">Status</span>
                    </div>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="repeater-row">
                    <div class="repeater-cell">
                        <asp:Label ID="pid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Label ID="DescriptionLabel" runat="server" CssClass="data-label" Text='<%# Eval("fee_description") %>'></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Label ID="AmountLabel" runat="server" CssClass="data-label" Text='<%# Eval("amount") %>'></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Label ID="CurrentDateLabel" runat="server" CssClass="data-label" Text='<%# Eval("declare_date") %>'></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Label ID="LastDateLabel" runat="server" CssClass="data-label" Text='<%# Eval("last_date") %>'></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Label ID="statusLabel" runat="server" CssClass="data-label" Text='<%# Eval("status") %>'></asp:Label>
                    </div>
                    <div class="repeater-cell">
                        <asp:Button ID="Pay" runat="server" CssClass="bn632-hover bn26" Text="Pay Now" CommandArgument='<%# Eval("Amount") %>' OnClick="Pay_Click" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div id="EditEmailModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <!-- Add your modal content here -->
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Email</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="currentemail" ReadOnly="true" runat="server" CssClass="custom" Text=""></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="red" ErrorMessage="Enter Valid Email" ControlToValidate="txtemail" Display="Dynamic" ValidationExpression="^\S+@\S+$"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtemail" runat="server" CssClass="custom" placeholder="Enter New Email"></asp:TextBox>
                        <asp:TextBox ID="txtpassword" runat="server" CssClass="custom" placeholder="Enter Password"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSavePayment" runat="server" CssClass="custombutton" Text="Change" OnClick="btnSavePayment_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="EditpasswordModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Add your modal content here -->
                <div class="modal-header">
                    <h4 class="modal-title">Edit Password</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="currentpassword" runat="server" CssClass="custom" placeholder="Enter Current Password"></asp:TextBox>
                    <asp:TextBox ID="newpassword" runat="server" CssClass="custom" placeholder="Enter New Password"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnpassword" runat="server" CssClass="custombutton" Text="Save" OnClick="btnpassword_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
