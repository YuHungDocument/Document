﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTest.aspx.cs" Inherits="WebApplication1.EmailTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            收件人：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            主旨：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            內文：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="寄出" />
            <asp:Label ID="Label1" runat="server" Text="成功" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
