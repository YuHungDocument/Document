<%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true"  CodeBehind="setInfo.aspx.cs" Inherits="WebApplication1.setInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
    <label for="exampleInputEmail1">
    使用者名稱:</label><asp:Label ID="LbId" runat="server" Text="Label"></asp:Label>
</div>
<div class="form-group">
    <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label">姓名</asp:Label>
    <div class="col-md-10">
        <asp:TextBox ID="UserName" runat="server" CssClass="auto-style1" Width="242px" />
    </div>
</div>
<div class="form-group">
    <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label">Email</asp:Label>
    <div class="col-md-10">
        <asp:TextBox ID="Email" runat="server" CssClass="auto-style1" TextMode="Email" Width="242px" />
    </div>
</div>
<div class="form-group">
    <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label">電話號碼</asp:Label>
    <div class="col-md-10">
        <asp:TextBox ID="Tel" runat="server" CssClass="auto-style1" TextMode="Phone" Width="239px" />
    </div>
</div>
<div class="form-group">
    <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label">手機號碼</asp:Label>
    <div class="col-md-10">
        <asp:TextBox ID="Cel" runat="server" CssClass="auto-style1" TextMode="Phone" Width="239px" />
    </div>
</div>
<div class="form-group">
    <div class="col-md-offset-2 col-md-10">
        <asp:Button ID="Button1" runat="server" class="btn btn-default" OnClick="Button1_Click" Text="修改" />
    </div>
</div>
</asp:Content>