<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Forget.aspx.cs" Inherits="WebApplication1.Forget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/<a href="Login.aspx">員工專區</a>/忘記密碼
    <br />
    <div style="margin: 0px auto;width: 40%; height: 30%; text-align: center;">
        <br />
        <asp:Label ID="Label1" runat="server" Text="請輸入員工工號或帳號" Font-Size="X-Large" ForeColor="#999999"></asp:Label>
        <br />
        <br />
            <asp:TextBox ID="Txt_Num" CssClass="form-control" placeholder="請輸入資料" runat="server"></asp:TextBox>        
        <br />
        <asp:Button ID="Btn_Go" CssClass="btn btn-success" runat="server" Text="送出" Font-Size="Large" OnClick="Btn_Go_Click" />
        
    </div>

</asp:Content>
