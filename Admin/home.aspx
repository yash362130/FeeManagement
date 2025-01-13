<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Async="true" Inherits="Fee_Management.Admin.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        h1 {
            color: #007bff;
            font-size: 32px;
            text-transform: uppercase;
            margin: 20px 0;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        .payment-item {
            border: 1px solid #ccc;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            animation: slideInFade 0.8s ease-out forwards;
            display: ruby-text-container;
            display: block;
            margin-bottom: 10px;

            font-size: 16px;
            justify-content: initial;
            margin-left: 10%;
            margin-right: 10%;
            display: flex;
            flex-wrap: wrap;
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
                flex-direction: column;
                padding: 10px;
                width: 90%;
            }

            .lbl {
                margin-right: 0; /* Remove right margin for better spacing on small screens */
                margin-bottom: 10px; /* Add bottom margin for better spacing on small screens */
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
            flex: 1; /* Distribute space equally among labels */
            max-width: 100%; /* Ensure labels take up the available width */
            margin-right: 10px; /* Adjust margin between labels */
            margin-bottom: 20px;
        }

        .no-record-message {
            color: red;
            font-size: 32px;
            text-transform: uppercase;
            margin: 20px 0;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        .bn54span {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #007bff;
            color: #fff;
            border: 2px solid transparent;
            border-radius: 5px;
            cursor: pointer;
            transition: all 0.3s ease;
            outline: none;
            position: relative;
            overflow: hidden;
            margin-left: 1%;
        }

            .bn54span::before {
                content: '';
                position: absolute;
                top: 50%;
                left: 50%;
                width: 300%;
                height: 300%;
                background-color: #fff;
                transition: all 0.3s ease;
                border-radius: 50%;
                z-index: 0;
                transform: translate(-50%, -50%);
                opacity: 0;
            }

            .bn54span:hover::before {
                width: 0;
                height: 0;
                opacity: 0.5;
            }

            .bn54span:hover {
                color: #007bff;
                border-color: #007bff;
            }

            .bn54span span {
                position: relative;
                z-index: 1;
            }

        @media only screen and (max-width: 768px) {
            .bn54span {
                font-size: 14px;
                padding: 8px 16px;
            }
        }

        .logout {
            padding: 10px 20px;
            font-size: 16px;
            background-color: red;
            color: #fff;
            border: 2px solid transparent;
            border-radius: 5px;
            cursor: pointer;
            transition: all 0.3s ease;
            outline: none;
            position: relative;
            overflow: hidden;
            margin-left:60%;
        }

            .logout::before {
                content: '';
                position: absolute;
                top: 50%;
                left: 50%;
                width: 300%;
                height: 300%;
                background-color: #fff;
                transition: all 0.3s ease;
                border-radius: 50%;
                z-index: 0;
                transform: translate(-50%, -50%);
                opacity: 0;
            }

            .logout:hover::before {
                width: 0;
                height: 0;
                opacity: 0.5;
            }

            .logout:hover {
                color: white;
                border-color: #007bff;
            }

            .logout span {
                position: relative;
                z-index: 1;
            }

        @media only screen and (max-width: 768px) {
            .logout {
                font-size: 14px;
                padding: 8px 16px;
            }
        }

        .label-style {
            font-weight: bold;
            font-size: 18px;
            color: #333; /* Change the color to your preference */
            /* Add more styling properties as needed */
        }

        /* Center align the table */
        .table-container {
            width: 50%;
            margin: 0 auto;
        }

        /* Style for the table */
        .my-table {
            border-collapse: collapse;
            width: 100%;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

            /* Style for table headers */
            .my-table th {
                border: 1px solid #ccc;
                padding: 12px;
                text-align: left;
                background-color: #f2f2f2;
                transition: background-color 0.3s ease;
            }

        /* Style for the table header */
        .table-header {
            text-align: center;
            font-size: 20px;
            padding: 16px 0; /* Added padding for header */
        }

        /* Hover effect for headers */
        .my-table th:hover {
            background-color: #e0e0e0;
        }

        /* Style for table cells */
        .my-table td {
            border: 1px solid #ccc;
            padding: 12px;
        }

        /* Alternate row background color */
        .my-table tbody tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        /* Animation for table rows */
        .my-table tbody tr {
            opacity: 0;
            animation: fade-in 0.6s ease forwards;
        }

        @keyframes fade-in {
            from {
                opacity: 0;
                transform: translateY(10px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>Payment Information</h1>
            <asp:Button ID="Daily" class="bn54span" runat="server" Text="Daily" OnClick="Daily_Click" />
            <asp:Button ID="Weekly" class="bn54span" runat="server" Text="Weekly" OnClick="Weekly_Click" />
            <asp:Button ID="Monthly" class="bn54span" runat="server" Text="Monthly" OnClick="Monthly_Click" />
            <asp:Button ID="year" runat="server" class="bn54span" Text="year" OnClick="year_Click" />
            <asp:Button ID="Logout" runat="server" Text="Logout" class="logout" OnClick="Logout_Click" />
            <div id="jsonContainer" runat="server">
                <div id="Container" class="animation-container"></div>
                <div>
                    <canvas id="myChart" style="max-width: 600px; margin: 20px auto;"></canvas>
                </div>
            </div>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <div class="table-container">
                        <table class="my-table">
                            <thead>
                                <tr>
                                    <th colspan="2" class="table-header">Student Information</th>
                                </tr>
                                <tr>
                                    <th>Course Name</th>
                                    <th>Student Count</th>
                                </tr>
                            </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("StudentCount") %></td>
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                   </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Panel ID="Panel1" Style="margin-bottom: 5%; margin-top: 5%; border: 1px solid; margin-left: 2%" Height="370" Width="96%" runat="server" ScrollBars="Vertical">
                <div class="payment-panel">
                    <asp:Repeater ID="paymentinfo" runat="server">
                        <HeaderTemplate>
                            <div class="payment-item">
                                <span class="lbl" style="font-weight: bold;">Name</span>
                                <span class="lbl" style="font-weight: bold;">Course</span>
                                <span class="lbl" style="font-weight: bold;">Payment Id</span>
                                <span class="lbl" style="font-weight: bold;">Amount</span>
                                <span class="lbl" style="font-weight: bold;">Payment Status</span>
                                <span class="lbl" style="font-weight: bold;">CreationDate</span>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="payment-item">
                                <asp:Label ID="name" runat="server" CssClass="lbl" Text='<%# Eval("student_name") %>'></asp:Label>
                                <asp:Label ID="course" runat="server" CssClass="lbl" Text='<%# Eval("course") %>'></asp:Label>
                                <asp:Label ID="paymentid" runat="server" CssClass="lbl" Text='<%# Eval("orderid") %>'></asp:Label>
                                <asp:Label ID="amount" runat="server" CssClass="lbl" Text='<%# Eval("amount") %>'></asp:Label>
                                <asp:Label ID="status" runat="server" CssClass="lbl" Text='<%# Eval("status") %>'></asp:Label>
                                <asp:Label ID="creationdate" runat="server" CssClass="lbl" Text='<%# Eval("creationdate") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
