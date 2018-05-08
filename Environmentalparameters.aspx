<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Environmentalparameters.aspx.cs" Inherits="WebApplication1.Environmentalparameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        企業名稱:<asp:Label ID="Lbl_ComName" runat="server"></asp:Label>
        <asp:TextBox ID="Txt_ComName" runat="server" Visible="False"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btn_change" runat="server" OnClick="Button1_Click" Text="變更公司名稱" />
    </p>
</asp:Content>
