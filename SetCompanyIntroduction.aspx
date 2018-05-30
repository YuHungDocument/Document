<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="SetCompanyIntroduction.aspx.cs" Inherits="WebApplication1.SetCompanyIntroduction" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <p></p>
    <CKEditor:CKEditorControl ID="txt_CI" runat="server"></CKEditor:CKEditorControl>
    <p></p>
     <asp:Button ID="Btn_Edit" runat="server" Text="修改" OnClick="Btn_Edit_Click" />
</asp:Content>
