<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="NewsEditDetail.aspx.cs" Inherits="WebApplication1.NewsEditDetail" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style1 {
            width: 102px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">公告標題</td>
            <td><asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox></td>            
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">公告類型</td>
            <td><asp:DropDownList ID="Ddl_Type" runat="server" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN" ></asp:DropDownList></td>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="NT" Name="Tp" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">公告內容</td>
            <td><CKEditor:CKEditorControl ID="Txt_Context" runat="server"></CKEditor:CKEditorControl></td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Button ID="Btn_Edit" runat="server" Text="修改" OnClick="Btn_Edit_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <input type="button" onclick="javascript:window.history.go(-1);"value="返回上一頁">
    <p></p>
</asp:Content>
