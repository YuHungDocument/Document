<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="BulletinDetail.aspx.cs" Inherits="WebApplication1.BulletinDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            hr{
                border-color:blue;
                border-width: 5px;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Lbl_Title" runat="server" Font-Size="XX-Large"></asp:Label>
    &nbsp; <asp:Label ID="Lbl_Dep" runat="server" ForeColor="#999999" Font-Size="Large"></asp:Label>
    <asp:Label ID="Lbl_Date" runat="server"></asp:Label>
    <hr />
    <asp:Label ID="Lbl_Context" runat="server"></asp:Label>
</asp:Content>
