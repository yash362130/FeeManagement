<%@ Page Title="" Language="C#" MasterPageFile="~/userside.Master" AutoEventWireup="true" CodeBehind="Errorpage.aspx.cs" Inherits="Fee_Management.User.Errorpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
      /*  body {
            font-family: Arial, sans-serif;
            background-color: #f8f8f8;
            margin: 0;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }*/

        h1 {
            color: #e74c3c; /* Red color for error message */
            font-size: 3em;
            margin-bottom: 20px;
        }

        p {
            color: #333;
            font-size: 1.5em;
        }

        a {
            color: #3498db; /* Blue color for link */
            text-decoration: none;
            font-weight: bold;
            transition: color 0.3s ease;
        }

        a:hover {
            color: #1d5cb9; /* Darker blue on hover */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Error</h1>
</asp:Content>
