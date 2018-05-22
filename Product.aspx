﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebApplication1.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
        <ul>
            <li><a style="color: #ff6a00" href="#">所有車款</a></li>
            <li><a href="#">公路車</a></li>
            <li><a href="#">折疊車</a></li>
            <li><a href="#">折疊車</a></li>
        </ul>
    </div>
    <div class="col-sm-10">
        <asp:DataList ID="DataList1" runat="server" DataKeyField="PID" DataSourceID="SqlDataSource1" RepeatDirection="Horizontal" RepeatColumns="2">
            <ItemTemplate>
                <div style="text-align: center; padding-left:10px;">
                    <asp:ImageButton ID="ImageButton1" style="height:250px; max-width:100%" runat="server" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ProductImg")) %>' />
                    <br />
                    <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("ProductName") %>' />                    
                    <br />
                    <div style="color:#999999">建議售價：NT $
            <asp:Label ID="ProductPriceLabel" runat="server" Text='<%# Eval("ProductPrice") %>' ForeColor="#999999" /></div>
                    <br />
                </div>

            </ItemTemplate>

        </asp:DataList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

    </div>
</asp:Content>
