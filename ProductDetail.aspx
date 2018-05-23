<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="WebApplication1.ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>

        .auto-style1 {
            width: 348px;
        }

    </style>
    <br />
    <a href="Home.aspx">首頁</a>/<a href="Product.aspx">產品介紹</a>/<asp:Label ID="Lbl_NamePath" runat="server" Text="Label"></asp:Label>
    <hr />
    
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">
                <asp:Label ID="Lbl_Name" runat="server" ForeColor="#666666" Font-Size="X-Large"></asp:Label>
                <br />
                <asp:Label ID="Lbl_Type" runat="server" ForeColor="#996633"></asp:Label>
                                <br />
                <asp:Label ID="Lbl_Price" runat="server" ForeColor="#999999"></asp:Label>
            </td>
            <td><asp:Image ID="Image1" runat="server" Style="max-width: 100%; max-height: 50%;" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ProductImg")) %>'/></td>
        </tr>
    </table>
    <br />


    <table class="nav-justified">
        <tr>
            <td style="text-align:center;background-color:#fb9750;font-weight:bold;font-size:x-large">產品介紹</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Lbl_Context" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
