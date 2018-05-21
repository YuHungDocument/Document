<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="WebApplication1.ProductEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:DataList ID="DataList1" runat="server" DataKeyField="PID" DataSourceID="SqlDataSource1" RepeatDirection="Horizontal">
        <ItemTemplate>
            <br />
            產品名稱
            <br />
            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ProductName") %>' />
            <br />
            車款
            <br />
            <asp:Label ID="ProductTypeLabel" runat="server" Text='<%# Eval("ProductType") %>' />
            <br />
            價格:
            <asp:Label ID="ProductPriceLabel" runat="server" Text='<%# Eval("ProductPrice") %>' />
            <br />
            <asp:ImageButton ID="ImageButton1" Height="100px" Width="100px" runat="server" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ProductImg")) %>' />

            <br />
            <br />
        </ItemTemplate>

    </asp:DataList>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

    <br />

</asp:Content>
