<%@ Page Title="" Language="C#" MasterPageFile="~/userside.Master" AutoEventWireup="true" CodeBehind="history.aspx.cs" Inherits="Fee_Management.User.history" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f8f8f8;
        }

        .container {
            width: 80%;
            margin: 0 auto;
        }

        .history-container {
            background-color: #fff;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin-top: 20px;
            max-width: 80%; /* Set a max-width for responsiveness */
            margin: 20px auto; /* Center the container horizontally */
        }

        h2 {
            color: #333;
            margin-bottom: 15px; /* Add some space below the heading */
        }

        .history-list {
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

            .history-list li {
                margin-bottom: 10px;
                padding: 15px;
                background-color: #f2f2f2;
                border-radius: 5px;
                border: 1px solid #ddd;
                display: flex; /* Use flexbox for side-by-side labels */
                justify-content: space-between;
            }

        .lbl {
            margin-right: 10%; /* Adjust spacing between labels */
            flex: 1; /* Allow labels to grow and shrink */
        }

        .hlbl {
            font-weight: bold; /* Make the header labels bold */
            margin-right: 10%; /* Adjust spacing between header labels */
            flex: 1; /* Allow labels to grow and shrink */
        }

        /* Responsive Styles */
        @media (max-width: 768px) {
            .history-container {
                padding: 15px;
            }

            .history-list li {
                flex-direction: column; /* Stack labels vertically on small screens */
            }

            .lbl, .hlbl {
                margin-right: 0; /* Remove right margin on small screens */
                margin-bottom: 5px; /* Add bottom margin for spacing */
            }
        }

        .bn53 {
            background-color: cadetblue;
            /*padding: 7px;*/
            width: 100px;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            animation: bn53bounce 4s infinite;
            cursor: pointer;
        }

        @keyframes bn53bounce {
            5%, 50% {
                transform: scale(1);
            }

            10% {
                transform: scale(1);
            }

            15% {
                transform: scale(1);
            }

            20% {
                transform: scale(1) rotate(-5deg);
            }

            25% {
                transform: scale(1) rotate(5deg);
            }

            30% {
                transform: scale(1) rotate(-3deg);
            }

            35% {
                transform: scale(1) rotate(2deg);
            }

            40% {
                transform: scale(1) rotate(0);
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="history-container">
        <h2>History</h2>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <ul class="history-list">
                    <li>
                        <span class="hlbl">Name</span>
                        <span class="hlbl">Payment Id</span>
                        <span class="hlbl">fee_description</span>
                        <span class="hlbl">Amount</span>
                        <span class="hlbl">Creation Date</span>
                    </li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul class="history-list">
                    <li>
                        <asp:Label ID="name" CssClass="lbl" runat="server" Text='<%# Eval("student_name") %>'></asp:Label>
                        <asp:Label ID="paymentid" CssClass="lbl" runat="server" Text='<%# Eval("paymentid") %>'></asp:Label>
                        <asp:Label ID="description" CssClass="lbl" runat="server" Text='<%# Eval("fee_description") %>'></asp:Label>
                        <asp:Label ID="amount" CssClass="lbl" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                        <asp:Label ID="creationdate" CssClass="lbl" runat="server" Text='<%# Eval("creationdate") %>'></asp:Label>
                        <asp:Label ID="course" runat="server" Text='<%# Eval("course") %>' Visible="false"></asp:Label>
                        <asp:Button ID="download" runat="server" class="bn53" OnClick="download_Click" Text="Download" />
                    </li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
