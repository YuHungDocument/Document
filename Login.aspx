<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    </head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse" style="background-color: #666666">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="Home.aspx">
                        <asp:Label ID="Label1" runat="server" Text="電子公文暨投票系統" ForeColor="White" Font-Bold="True"></asp:Label></a>
                </div>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="Home.aspx" style="color: #FFFFFF"><span class="glyphicon glyphicon-home"></span>回首頁</a></li>

                </ul>
            </div>
        </div>
        <br />
        <br />
        <div style="border: thin solid #C0C0C0; padding: 15px; margin: auto; width: 400px;">
            <h3>會員登入</h3>
            <br />
            <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" ></asp:TextBox>
            <br />
            <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="Btn_Login" runat="server" Text="登入" class="btn btn-warning" Height="46px" Width="72px" OnClick="Btn_Login_Click" />
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
        </div>
        <br />
&nbsp;&nbsp;
    </form>
</body>
</html>
