<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Bulletin.aspx.cs" Inherits="WebApplication1.Bulletin" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body
        {
            font-size:large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
       &nbsp;公告標題：<asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;公告內容：<br /><br /><CKEditor:CKEditorControl ID="txt_Connect" runat="server">
</CKEditor:CKEditorControl>
    <br />
    &nbsp;&nbsp;<asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" Font-Size="Large" />
&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="返回"  Font-Size="Large"/>
</asp:Content>
