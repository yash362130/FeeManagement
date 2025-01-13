<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="fee_information.aspx.cs" Inherits="Fee_Management.Admin.fee_information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        .dropdown-container {
            margin-top: 20px;
            margin-bottom: 20px;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
        }

        .btn-create {
            padding: 10px 20px;
            background-color: #337ab7;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .custom-dropdown {
            width: 200px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #fff;
            color: #333;
            font-size: 14px;
            outline: none;
        }

            .custom-dropdown option {
                background-color: #fff;
                color: #333;
            }

        .payment-item {
            border: 1px solid #ccc;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            animation: slideInFade 0.8s ease-out forwards;
            display: ruby-text-container;
            display: flex;
            margin-bottom: 10px;
            font-size: 16px;
            justify-content: initial;
            margin-left: 2%;
            margin-right: 2%;
            width: 96%;
        }

        @keyframes slideInFade {
            from {
                opacity: 0;
                transform: translateX(-10px);
            }

            to {
                opacity: 1;
                transform: translateX(0);
            }
        }

        @media screen and (max-width: 768px) {
            .payment-item {
                padding: 10px;
                width: 90%;
            }
        }

        .payment-item-header {
            font-weight: bold;
            margin-bottom: 5px;
        }

        .payment-item-amount {
            font-size: 1.2em;
        }

        .payment-item-details {
            font-size: 0.9em;
            color: #666;
        }

        .payment-panel {
            margin: 0 auto;
            max-width: 100%;
            margin-top: 3%;
            margin-bottom: 5%;
        }

        .payment-item-header {
            font-weight: bold;
            margin-bottom: 5px;
        }

        .payment-item-amount {
            font-size: 1.2em;
        }

        .payment-item-details {
            font-size: 0.9em;
            color: #666;
        }

        .lbl {
            margin-right: 6%;
            flex: 1;
            justify-items: center;
            max-width: 50%;
        }

        .hlbl {
            margin-right: 6%;
            flex: 1;
            justify-items: center;
            max-width: 40%;
            font-weight: bold;
        }

        .pdf {
            align-items: center;
            font-family: inherit;
            font-weight: 500;
            font-size: 16px;
            padding: 0.7em 1.4em 0.7em 1.1em;
            color: white;
            background: #ad5389;
            background-color: DodgerBlue;
            border: none;
            box-shadow: 0 0.7em 1.5em -0.5em #14a73e98;
            letter-spacing: 0.05em;
            border-radius: 20em;
            cursor: pointer;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

        .pdf {
            box-shadow: 0 0.5em 1.5em -0.5em #14a73e98;
        }

        .pdf {
            box-shadow: 0 0.3em 1em -0.5em #14a73e98;
        }

        .pay {
            align-items: center;
            font-family: inherit;
            font-weight: 500;
            font-size: 16px;
            padding: 0.7em 1.4em 0.7em 1.1em;
            color: white;
            background: #ad5389;
            background: linear-gradient(0deg, rgba(20,167,62,1) 0%, rgba(102,247,113,1) 100%);
            border: none;
            box-shadow: 0 0.7em 1.5em -0.5em #14a73e98;
            letter-spacing: 0.05em;
            border-radius: 20em;
            cursor: pointer;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

        .pay {
            box-shadow: 0 0.5em 1.5em -0.5em #14a73e98;
        }

        .pay {
            box-shadow: 0 0.3em 1em -0.5em #14a73e98;
        }

        .delete {
            align-items: center;
            font-family: inherit;
            font-weight: 500;
            font-size: 16px;
            padding: 0.7em 1.4em 0.7em 1.1em;
            color: white;
            background: #ad5389;
            background: linear-gradient(0deg, rgb(255 0 0) 0%, rgb(255 0 0) 100%);
            border: none;
            box-shadow: 0 0.7em 1.5em -0.5em #14a73e98;
            letter-spacing: 0.05em;
            border-radius: 20em;
            cursor: pointer;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

        .delete {
            box-shadow: 0 0.5em 1.5em -0.5em #14a73e98;
        }

        .delete {
            box-shadow: 0 0.3em 1em -0.5em #14a73e98;
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
    <script type="text/javascript">
        function toggleTextBox(rowIndex) {
            var txtAdditional = document.getElementById('<%= feeinformation.ClientID %>_ctl' + rowIndex + '_txtAdditional');
            if (txtAdditional.style.display === "none") {
                txtAdditional.style.display = "block";
            } else {
                txtAdditional.style.display = "none";
            }
        }
    </script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function showPayModal() {
            // Use jQuery to show the modal
            $('#payModal').modal('show');
        }

        function closePayModal() {
            // Use jQuery to hide the modal
            $('#payModal').modal('hide');
            console.log("Modal closed");
        }

        window.onclick = function (event) {
            var modal = document.getElementById('payModal');
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        };
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="dropdown-container">
                <asp:Label ID="lblcourse" runat="server" Text="Select Course:"></asp:Label>
                <asp:DropDownList ID="courseDropdown" CssClass="custom-dropdown" runat="server" Required="required" AutoPostBack="true" OnSelectedIndexChanged="courseDropdown_SelectedIndexChanged">
                    <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
                </asp:DropDownList><br />
                <asp:Label ID="feedesc" runat="server" Text="Select Fee Type"></asp:Label>
                <asp:DropDownList ID="feeDescriptionDropdown" CssClass="custom-dropdown" runat="server" Required="required" AutoPostBack="true" OnSelectedIndexChanged="feeDescriptionDropdown_SelectedIndexChanged">
                    <asp:ListItem Text="Select Fee Description" Value=""></asp:ListItem>
                </asp:DropDownList><br />
                <asp:Button ID="showdata" runat="server" class="pay" Text="Show Data" OnClick="showdata_Click" />
            </div>
            <div class="payment-panel">
                <asp:Repeater ID="feeinformation" runat="server" OnItemDataBound="feeinformation_ItemDataBound">
                    <HeaderTemplate>
                        <div class="payment-item">
                            <span class="hlbl">Student Name</span>
                            <span class="hlbl">Phone Number</span>
                            <span class="hlbl">Amount</span>
                            <span class="hlbl">Lastdate</span>
                            <span class="hlbl">Status</span>
                            <span class="hlbl">Action</span>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="payment-item">
                            <asp:Label ID="email" runat="server" Text='<%# Eval("email") %>' Visible="false"></asp:Label>
                            <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="studentname" class="lbl" runat="server" Text='<%# Eval("student_name") %>'></asp:Label>
                            <asp:Label ID="phonenumber" class="lbl" runat="server" Text='<%# Eval("phone_number") %>'></asp:Label>
                            <asp:Label ID="amount" class="lbl" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                            <asp:Label ID="lastdate" class="lbl" runat="server" Text='<%# Eval("last_date") %>'></asp:Label>
                            <asp:Label ID="status" class="lbl" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                            <asp:Button ID="pay" runat="server" class="pay" Text="Pay" OnClick="pay_Click" />
                            <asp:Button ID="delete" runat="server" class="delete" Text="Delete" OnClick="delete_Click" />
                            <asp:Label ID="note" runat="server" Text="" class="lbl" Visible="false"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="payModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Add your modal content here -->
                <div class="modal-header">
                    <h4 class="modal-title">Payment Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtPayAmount" runat="server" CssClass="custom" placeholder="Enter Refrabce Number"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnSavePayment" runat="server" CssClass="custombutton" Text="Save" OnClick="btnSavePayment_Click1" />
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="downloadBtn" runat="server" class="pdf" Text="Download PDF" OnClick="downloadBtn_Click" />
</asp:Content>
