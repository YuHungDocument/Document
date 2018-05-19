<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="AddNewsMessage.aspx.cs" Inherits="WebApplication1.AddNewsMessage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
       &nbsp;公告標題：<asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox>
<br />
    &nbsp;公告類型：<asp:DropDownList ID="Dp_Type" runat="server">
        <asp:ListItem>-請選擇公告類型-</asp:ListItem>
        <asp:ListItem>系統公告</asp:ListItem>
        <asp:ListItem>更新消息</asp:ListItem>
        <asp:ListItem>新聞發布</asp:ListItem>
        <asp:ListItem>其他訊息</asp:ListItem>
    </asp:DropDownList>
<br />
&nbsp;公告內容：    <CKEditor:CKEditorControl ID="txt_Connect" runat="server"></CKEditor:CKEditorControl>
    <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" />
&nbsp; 
</asp:Content>
