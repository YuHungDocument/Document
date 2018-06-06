<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="KeyAddress.aspx.cs" Inherits="WebApplication1.KeyAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        * {
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }



        /* Create three equal columns that floats next to each other */
        .column {
            float: left;
            width: 60%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column1 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column2 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Style the footer */
        .footer {
            background-color: #f1f1f1;
            padding: 10px;
            text-align: center;
        }

        /* Responsive layout - makes the three columns stack on top of each other instead of next to each other */
        @media (max-width: 600px) {
            .column {
                width: 100%;
            }
        }
        .auto-style1 {
            width: 357px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="column1" style="">
        </div>
        <div class="column" style="">

            <asp:Image ID="Image1" runat="server" ImageUrl="~/P/金鑰位置.png" Height="143px" Width="219px" />
            <br />
            <table style="font-size:large" class="nav-justified">
                <tr>
                    <td class="auto-style1">選擇金鑰位址:</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">輸入根目錄<asp:TextBox ID="root" Placeholder="Ex:C" runat="server"></asp:TextBox></td>
                    <td><asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
            </table>
            <div style="font-size:large">
                <br /><asp:Button ID="Button1" runat="server" Text="確認" OnClick="Button1_Click" />
            </div>
        </div>
        <div class="column2" style="">
        </div>
    </div>
</asp:Content>
