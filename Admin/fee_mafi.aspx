<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="fee_mafi.aspx.cs" Inherits="Fee_Management.Admin.fee_mafi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .addfee {
            border: 0;
            text-align: center;
            display: inline-block;
            padding: 14px;
            width: 100px;
            margin: 5px;
            color: #ffffff;
            background-color: #36a2eb;
            border-radius: 8px;
            font-family: "proxima -nova-soft", sans-serif;
            font-weight: 600;
            text-decoration: none;
            transition: box-shadow 200ms ease-out;
        }

        .cont {
            margin: auto;
            background-color: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            padding: 20px;
        }

        .tbl {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

            .tbl th, .tbl td {
                border: 1px solid #dee2e6;
                padding: 12px;
                text-align: left;
            }

            .tbl th {
                background-color: #007bff;
                color: #ffffff;
            }

            .tbl tr:nth-child(even) {
                background-color: #f8f9fa;
            }

            .tbl tr:not(:first-child) {
                margin-top: 10px;
            }

        hr {
            border: none;
            height: 1px;
            background-color: #dee2e6;
            margin: 20px 0;
        }

        .modalBackground {
            background-color: #000;
            filter: alpha(opacity=70);
            opacity: 0.7;
            z-index: 10000;
        }

        .popupPanel {
            background-color: #fff;
            border: 1px solid #ccc;
            padding: 10px;
            width: 300px;
            border-radius: 5px;
            box-shadow: 0px 0px 10px #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="cont">
                <table class="tbl">
                    <tr>
                        <th>Student Name</th>
                        <th>Email</th>
                        <th>Address</th>
                        <th>Phone Number</th>
                        <th>Course</th>
                        <th></th>
                    </tr>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="name" runat="server" class="lbl" Text='<%# Eval("name") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="email" runat="server" class="lbl" Text='<%#  Eval("email") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="address" runat="server" class="lbl" Text='<%# Eval("address") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="number" runat="server" class="lbl" Text='<%# Eval("phone_number") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="course" runat="server" class="lbl" Text='<%# Eval("course") %>'></asp:Label></td>
                                <td>
                                    <asp:Button ID="btnPopup" runat="server" class="addfee" Text="Remove" OnClick="btnPopup_Click"  CommandArgument='<%# Eval("email") %>' /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
