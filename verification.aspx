<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verification.aspx.cs" Inherits="WebApplication1.verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <link href="style.css" rel="stylesheet" media="all" />
    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div style="margin:0px auto; width:400px;height:400px;text-align:center;">
            <h1>金鑰獲取驗證</h1>
            <br />
            <br />
            <br />
            <asp:TextBox placeholder="請出入驗證碼" CssClass="form-control" ID="TextBox1" runat="server"></asp:TextBox>
            <p></p>
            <asp:Button ID="Btn_Send" runat="server" Text="送出驗證碼" OnClick="Btn_Send_Click" />
        </div>
    </form>
</body>
</html>
