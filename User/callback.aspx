<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="callback.aspx.cs" Inherits="Fee_Management.User.callback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <style>
        body {
            font-family: 'Ultra', sans-serif;
            background-color: #f8f8f8;
            text-align: center;
            margin: 20px;
            color: #333;
        }

        h1 {
            font-size: 36px;
            margin-bottom: 20px;
        }

        .label-container {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            margin-bottom: 20px;
        }

        .label-style {
            font-size: 18px;
            color: #4CAF50;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .link-container {
            margin-top: 20px;
        }

        p {
            font-size: 16px;
            margin: 10px 0;
        }

        a {
            display: inline-block;
            padding: 10px 20px;
            background-color: #4CAF50;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            font-size: 18px;
            transition: background-color 0.3s ease;
        }

        a:hover {
            background-color: #45a049;
        }

        @media (max-width: 768px) {
            .label-container {
                width: 80%;
                margin: 0 auto;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 runat="server" id="h1Message"></h1>
        <div class="label-container">
            <asp:Label ID="DPaymentid" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="pAmount" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="email" runat="server" Text=""></asp:Label>
        </div>
        <div class="link-container">
            <p>click here to go to home</p>
            <p><a href="user_information.aspx" role="button">home</a></p>
        </div>
    </form>
</body>
</html>
