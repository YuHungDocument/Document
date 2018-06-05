<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="BulletinDetail.aspx.cs" Inherits="WebApplication1.BulletinDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        hr {
            border-width: 2px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Lbl_Title" runat="server" Font-Size="XX-Large"></asp:Label>
    &nbsp;
    <asp:Label ID="Lbl_Dep" runat="server" ForeColor="#999999" Font-Size="Large"></asp:Label>
    <asp:Label ID="Lbl_Date" runat="server"></asp:Label>
    <hr />
    <asp:Label ID="Lbl_Context" runat="server"></asp:Label>
    <br />
    <br />
    <hr />
   <input type="button" onclick="javascript:window.history.go(-1);"value="返回上一頁">
    <br />

</asp:Content>
