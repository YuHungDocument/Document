<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="CompanyIntroduction.aspx.cs" Inherits="WebApplication1.CompanyIntroduction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/公司簡介
    <hr />
    <asp:Image ID="Image1" Style="height:350px;width:100%" runat="server" ImageUrl="~/P/綠.jpg" />
    <p></p>
    <br />
    <asp:Label ID="Lbl_CI" runat="server" Text="Label" ></asp:Label>
</asp:Content>
