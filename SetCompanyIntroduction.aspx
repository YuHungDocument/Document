<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="SetCompanyIntroduction.aspx.cs" Inherits="WebApplication1.SetCompanyIntroduction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txt_CI" runat="server" ForeColor="#0094FF" Height="200px" Width="100%"></asp:TextBox>
     <asp:Button ID="Btn_Edit" runat="server" Text="修改" OnClick="Btn_Edit_Click" />
</asp:Content>
