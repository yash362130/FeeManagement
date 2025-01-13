<%@ Page Title="" Language="C#" MasterPageFile="~/Design.Master" AutoEventWireup="true" CodeBehind="delete_student.aspx.cs" Inherits="Fee_Management.Admin.delete_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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

        .student {
            margin-bottom: 20px;
            border: 1px solid #ccc;
            padding: 10px;
            border-radius: 5px;
        }

            .student h3 {
                margin-bottom: 5px;
                font-size: 18px;
                color: #333;
            }

            .student p {
                margin-bottom: 3px;
                font-size: 14px;
                color: #666;
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
            justify-content: space-between;
            margin-left: 10%;
            margin-right: 10%;
        }

        .lbl {
            flex: 1;
            justify-content: center;
            max-width: 50%;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../CSS/update_class.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="center-container">
                <div class="form-group">
                    <h1>Delete Student</h1>
                    <br />
                    <div class="inline-container">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="deletestudent" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Course:"></asp:Label>
                        <asp:DropDownList ID="deletestudent" runat="server" CssClass="input-field" ClientIDMode="Static">
                            <asp:ListItem Text="Select Course" Value=""></asp:ListItem>
                        </asp:DropDownList><br />
                    </div>
                    <asp:Button ID="show_student" runat="server" CssClass="submit-button" Text="Show" OnClick="show_student_Click" /><br />
                    <br />
                </div>
            </div>
            <asp:Repeater ID="StudentRepeater" runat="server">
                <HeaderTemplate>
                    <div class="payment-item" style="font-weight:bold;">
                        <span style="margin-left:90px;">Student Name</span>
                        <span>Email</span>
                        <span style="margin-left:50px;">Phone Number</span>
                        <span></span>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="payment-item">
                        <asp:Label ID="name" runat="server" class="lbl" Text='<%#Eval("name") %>'></asp:Label>
                        <asp:Label ID="email" runat="server" class="lbl" Text='<%#Eval("email") %>'></asp:Label>
                        <asp:Label ID="number" runat="server" class="lbl" Text='<%#Eval("phone_number") %>'></asp:Label>
                        <asp:Button ID="Delete" runat="server" class="delete" Text="Delete" OnClick="Delete_Click" CommandArgument='<%# Eval("email") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="ErrorMessage" runat="server" Text="" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
