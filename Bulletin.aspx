<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Bulletin.aspx.cs" Inherits="WebApplication1.Bulletin" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;公告標題：<asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox>
<br />
    &nbsp;員工編號：<asp:Label ID="Lbl_EID" runat="server"></asp:Label>
<br />
&nbsp;公告部門：<asp:Label ID="Lbl_Dp" runat="server"></asp:Label>
<br />
&nbsp;公告內容：<CKEditor:CKEditorControl ID="txt_Connect" runat="server">
</CKEditor:CKEditorControl>
    <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" />
&nbsp; 
</asp:Content>
