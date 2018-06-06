<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Remove_employee.aspx.cs" Inherits="WebApplication1.Remove_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/all.css" rel="stylesheet" />
    <div class="sub1" >
        <asp:DropDownList ID="DropDownList1" CssClass="DDL" runat="server" DataSourceID="SqlDataSource1" DataTextField="Department" DataValueField="Department" AutoPostBack="True"></asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" CssClass="DDL" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="EID" AutoPostBack="True"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString2 %>" SelectCommand="SELECT DISTINCT [Department] FROM [UserInfo] ORDER BY [Department] DESC"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [EID], [Name], [Department], [position] FROM [UserInfo] WHERE ([Department] = @Department)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Department" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button ID="Button2" Cssclass="btn1" runat="server" Text="顯示詳細資料" OnClick="Button2_Click" /><br />
        <asp:Button ID="Button1" Cssclass="btn1" runat="server" Text="刪除" OnClick="Button1_Click" />
    </div>
    <div class="sub2">

        <asp:GridView CssClass="table" ID="show" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="員工編號">
                    <ItemTemplate>
                        <asp:Label ID="Lb_EID" runat="server" Text='<%# Eval("EID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名稱">
                    <ItemTemplate>
                        <asp:Label ID="Lb_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="單位">
                    <ItemTemplate>
                        <asp:Label ID="Lb_Dp" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="職稱">
                    <ItemTemplate>
                        <asp:Label ID="LB_Po" runat="server" Text='<%# Eval("position") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT DISTINCT [EID], [Name], [Department], [position] FROM [UserInfo] WHERE (([Name] = @Name) AND ([EID] = @EID))">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList2" Name="Name" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="DropDownList1" Name="EID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>
