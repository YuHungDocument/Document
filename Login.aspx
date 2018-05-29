<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>


        .auto-style1 {
            margin-left: 452;
        }
        .auto-style2 {
            margin-left: 778;
        }


    </style>

    <script> 

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/員工專區
    <hr />
    <br />
        <div style="text-align: center;">
            <h1 style="text-align: center"><b>員工登入</b></h1>
            <br />
            <br />
            <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" Font-Size="Large" CssClass="auto-style1" Height="47px" Width="370px"></asp:TextBox>
            <br />
            <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Font-Size="Large" CssClass="auto-style2" Height="54px" Width="374px"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="登入" class="button"  OnClick="Btn_Login_Click" Font-Size="XX-Large" />
            
        

            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server">忘記密碼</asp:LinkButton>


    </div>

</asp:Content>
