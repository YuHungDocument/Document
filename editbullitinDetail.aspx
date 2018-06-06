<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="editbullitinDetail.aspx.cs" Inherits="WebApplication1.editbullitinDetail" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">

        .auto-style1 {
            width: 102px;
        }

         .auto-style2 {
             width: 102px;
             height: 20px;
         }
         .auto-style3 {
             height: 20px;
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style2">公告標題</td>
            <td class="auto-style3"><asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox></td>            
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
