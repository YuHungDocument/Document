<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="AddNewsMessage.aspx.cs" Inherits="WebApplication1.AddNewsMessage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            &nbsp;公告標題：<asp:TextBox ID="Txt_Title" runat="server"></asp:TextBox>
            <br />
            <br />
            &nbsp;公告類型：<asp:DropDownList ID="Dp_Type" runat="server" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="NT" Name="Tp" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <br />
            &nbsp;公告內容：   
    <CKEditor:CKEditorControl ID="txt_Connect" runat="server"></CKEditor:CKEditorControl>
            <br />
            
            &nbsp; 
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="送出" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Btn_Return" runat="server"  Text="返回" OnClick="Btn_Return_Click" />
    <p></p>
</asp:Content>
