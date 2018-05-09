<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    </style>

    <script> 

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h1 style="text-align:center"><b>使用者登入</b></h1>
    <br />
    <div class="container" style="border:solid 1px #ccc; text-align:center;margin:0px auto; height:300px;width:400px; border-radius:10px;">
        
                          <br />
        <br />
        <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server"  Font-Size="Large"></asp:TextBox>
        <br />
        <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password"  Font-Size="Large"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="登入" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Login_Click" Font-Size="Large" />
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="註冊" class="btn btn-info" Height="36px" Width="69px" OnClick="Button2_Click" Font-Size="Large" />

        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server">忘記密碼</asp:LinkButton>

    </div>

</asp:Content>
