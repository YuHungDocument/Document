<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Remove_employee.aspx.cs" Inherits="WebApplication1.Remove_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Department" DataValueField="Department" AutoPostBack="True"></asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="EID" AutoPostBack="True"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString2 %>" SelectCommand="SELECT DISTINCT [Department] FROM [UserInfo] ORDER BY [Department] DESC"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [Department], [Name], [EID], [position] FROM [UserInfo] WHERE ([Department] = @Department)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Department" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" Text="刪除" OnClick="Button1_Click" />
    </div>
    <div>
        <asp:GridView ID="Show" runat="server" DataSourceID="SqlDataSource3"></asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [EID], [Name], [Department], [position] FROM [UserInfo] WHERE (([Department] = @Department) AND ([EID] = @EID))">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="Department" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="DropDownList2" Name="EID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
