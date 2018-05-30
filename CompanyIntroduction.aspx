<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="CompanyIntroduction.aspx.cs" Inherits="WebApplication1.CompanyIntroduction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/公司簡介
    <hr />
    <asp:Label ID="Lbl_CI" runat="server" Text="Label" ForeColor="#0094FF"></asp:Label>
</asp:Content>
