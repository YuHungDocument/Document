<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="Environmentalparameters.aspx.cs" Inherits="WebApplication1.Environmentalparameters" %>
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
     <div class="sidenav col-sm-2">
        <h3>參數設定</h3>
        <ul>
            <li><a href="Environmentalparameters.aspx">企業名稱設定</a></li>
            <li><a href="SetConnect.aspx">聯絡問題設定</a></li>
           <li><a href="SetBT.aspx">產品種類設定</a></li>
            <li><a href="SetNT.aspx">最新消息類別設定</a></li>
        </ul>
    </div>
     <p>
    <p>
        企業名稱:<asp:Label ID="Lbl_ComName" runat="server"></asp:Label>
        <asp:TextBox ID="Txt_ComName" runat="server" Visible="False"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btn_change" runat="server" OnClick="Button1_Click" Text="變更公司名稱" />
    </p>
</asp:Content>
