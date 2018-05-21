<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="ProductAdd.aspx.cs" Inherits="WebApplication1.ProductAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 183px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">產品名稱</td>
            <td><asp:TextBox ID="Txt_Name" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    

        <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Btn_UpLoad" runat="server" Text="新增產品" OnClick="Btn_UpLoad_Click" />
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:HyperLink ID="HyperLink1" runat="server">點選查看上傳圖片</asp:HyperLink>
</asp:Content>
