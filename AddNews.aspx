<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="WebApplication1.AddNews" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
       &nbsp;公告標題：<asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox>
<br />
    &nbsp;公告類型：<asp:DropDownList ID="Dp_Type" runat="server" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN">
    </asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:電子公文ConnectionString %>' SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
        <SelectParameters>
            <asp:Parameter DefaultValue="NT" Name="Tp" Type="String"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
&nbsp;公告內容：<CKEditor:CKEditorControl ID="txt_Connect" runat="server">
</CKEditor:CKEditorControl>
    <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" />
&nbsp; 
</asp:Content>
