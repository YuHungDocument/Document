<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="WebApplication1.ProductEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            border: solid 1px #00b465;
            background-color: white;
            color: #00b465;
        }

                .btn:hover {
            border: solid 1px #ffffff;
            background-color: #00b465;
            color: white;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container" style="text-align:center">
        <asp:Button CssClass="btn" ID="Btn_Inset" runat="server" Text="新增產品" OnClick="Btn_Inset_Click" />
    </div>
    <br />
    <br />
    <asp:DataList ID="DataList1" runat="server" DataKeyField="PID" DataSourceID="SqlDataSource1" RepeatDirection="Horizontal" RepeatColumns="3">
        <ItemTemplate>
            <div style="text-align: center; padding-left: 10px;">
                <asp:ImageButton ID="ImageButton1" Style="height: 200px; max-width: 100%" runat="server" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ProductImg")) %>' />
                <br />
                產品名稱：
            <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ProductName") %>' />
                <br />
                車款：
            <asp:Label ID="ProductTypeLabel" runat="server" Text='<%# Eval("ProductType") %>' />
                <br />
                Nt $
            <asp:Label ID="ProductPriceLabel" runat="server" Text='<%# Eval("ProductPrice") %>' />
                <asp:Label ID="Lbl_ID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                <br />
                <asp:LinkButton ID="Lb_Edit" runat="server" OnClick="Lb_Edit_Click">產品修改</asp:LinkButton>
                <br />
            </div>
        </ItemTemplate>

    </asp:DataList>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

    <br />

</asp:Content>
