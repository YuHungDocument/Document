<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebApplication1.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table a{
            color: #000000;
            text-decoration: none; /* 不顯示預設底線 */
            font-size: x-large; /* 字體大小 x-large */
            display: block; /* 以區塊方式顯示 */
            line-height: 50px; /* 行高 50px，並垂直置中 */
            width: 100px; /* 寬度 100px */
            text-align: center; /* 文字文平置中 */
            font-weight: bold; /* 粗體字 */
            border-bottom: 1px dotted #CCC;
        }

        .table a:active{
            color:#ff6a00;
        }

        .sidenav ul {
            list-style-type: none; /* 不顯示清單符號 */
            margin: 0px;
            padding: 0px;
        }

        .sdienav ul li {
            float: left;
            background-color: #CCC; /* 每個項目的背景色 */
        }

        .sidenav ul li a {
            color: #000000;
            text-decoration: none; /* 不顯示預設底線 */
            font-size: x-large; /* 字體大小 x-large */
            display: block; /* 以區塊方式顯示 */
            line-height: 50px; /* 行高 50px，並垂直置中 */
            width: 100px; /* 寬度 100px */
            text-align: center; /* 文字文平置中 */
            font-weight: bold; /* 粗體字 */
            border-bottom: 1px dotted #CCC;
        }

            .sidenav ul li a:hover {
                color: #ff6a00
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/產品介紹
    <hr />
        <div class="sidenav col-sm-2">
       <h3>各類車種</h3>
     <asp:LinkButton ID="TNALL" runat="server" style="color: #000000;
            text-decoration: none; /* 不顯示預設底線 */
            font-size: x-large; /* 字體大小 x-large */
            display: block; /* 以區塊方式顯示 */
            line-height: 50px; /* 行高 50px，並垂直置中 */
            width: 100px; /* 寬度 100px */
            text-align: center; /* 文字文平置中 */
            font-weight: bold; /* 粗體字 */
            " Text="所有車款" OnClick="TNALL_Click"></asp:LinkButton>
          <p>
        
    <asp:DataList  ID="DataList2" class="table" runat="server" DataSourceID="SqlDataSource2">
        <ItemTemplate>
            <asp:LinkButton ID="TNLb" runat="server" Text='<%# Eval("TN") %>' OnClick="TNLb_Click"></asp:LinkButton>
            <br />
<br />
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp) AND [TID]!=0 ">
        <SelectParameters>
            <asp:Parameter DefaultValue="BT" Name="Tp" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
                  </div>
    <div class="col-sm-10">
        <asp:DataList ID="DataList1" runat="server" DataKeyField="PID" DataSourceID="SqlDataSource1" RepeatDirection="Horizontal" RepeatColumns="2">
            <ItemTemplate>
                <div style="text-align: center; padding-left:10px;">
                    <asp:ImageButton ID="ImageButton1" style="height:250px; max-width:100%" runat="server" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ProductImg")) %>' OnClick="ImageButton1_Click" />
                    <br />
                    <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ProductName") %>' />                    
                    <br />
                    <div style="color:#999999">建議售價：NT $
            <asp:Label ID="ProductPriceLabel" runat="server" Text='<%# Eval("ProductPrice") %>' ForeColor="#999999" /></div>
                    <asp:Label ID="Lbl_PID" runat="server" Text='<%# Eval("PID") %>' Visible="false"></asp:Label>
                    <br />
                </div>

            </ItemTemplate>

        </asp:DataList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

    </div>
</asp:Content>
