<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NEWHOME.aspx.cs" Inherits="WebApplication1.NEWHOME" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <!-- load stylesheets -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400">
    <!-- Google web font "Open Sans" -->
    <link rel="stylesheet" href="font-awesome-4.7.0/css/font-awesome.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- Bootstrap style -->
    <link rel="stylesheet" href="css/tooplate-style.css">
    <!-- Templatemo style -->
    <style>
        body {
            margin: 0;
        }

        .navbar {
            overflow: hidden; /* 自動隱藏超出的文字或圖片 */
            background-color: #333131; /* 背景顏色 */
            position: fixed; /* 元素位置固定 */
            top: 0; /* 距離頁面上方空格 */
            width: 100%; /* 左右寬度 */
        }

            .navbar a {
                float: left;
                display: block;
                color: #f2f2f2;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 17px;
            }

                .navbar a:hover {
                    background: #ddd;
                    color: black;
                }

        .main {
            padding: 16px;
            margin-top: 30px;
            height: 1200px; /* Used in this example to enable scrolling */
        }
    </style>
    <%--固定上方列--%>

    <style>
        * {
            box-sizing: border-box;
        }

        /* Add a gray background color with some padding */
        body {
            font-family: Arial;
            padding: 20px;
            background: #f1f1f1;
        }

        /* Header/Blog Title */
        .header {
            padding: 30px;
            font-size: 40px;
            text-align: center;
            background: white;
        }

        /* Create two unequal columns that floats next to each other */
        /* Left column */
        .leftcolumn {
            float: left;
            width: 75%;
        }

        /* Right column */
        .rightcolumn {
            float: left;
            width: 25%;
            padding-left: 20px;
        }

        /* Fake image */
        .fakeimg {
            background-color: #aaa;
            width: 100%;
            padding: 20px;
        }

        /* Add a card effect for articles */
        .card {
            background-color: white;
            padding: 20px;
            margin-top: 20px;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Footer */
        .footer {
            padding: 20px;
            text-align: center;
            background: #ddd;
            margin-top: 20px;
        }

        /* Responsive layout - when the screen is less than 800px wide, make the two columns stack on top of each other instead of next to each other */
        @media screen and (max-width: 800px) {
            .leftcolumn, .rightcolumn {
                width: 100%;
                padding: 0;
            }
        }
    </style>
    <%--各個區塊--%>

    <style>
        img {
            display: block;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <%--圖片--%>

    <style>
        * {
            box-sizing: border-box;
        }

        /* Create four equal columns that floats next to each other */
        .column {
            float: left;
            width: 10%;
            padding: 10px;
            height: 160px; /* Should be removed. Only for demonstration */
        }
        .column1 {
            float: left;
            width: 50%;
            padding: 10px;
            height: 160px; /* Should be removed. Only for demonstration */
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }
    </style><%--最下方列--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar" style="box-shadow: 1px 1px 3px gray; margin-right: 8px;">
            <a href="#NEWHOME" style="color: white; font-family: PMingLiU; font-weight: bold;">東嶽股份有限公司</a>
            <a href="#NEWHOME" style="color: white; font-family: PMingLiU; font-weight: bold;">首頁</a>
            <a href="#news" style="color: white; font-family: PMingLiU; font-weight: bold;">電子公文系統</a>
            <a href="#contact" style="color: white; font-family: PMingLiU; font-weight: bold;">公文系統使用說明</a>
        </div><%--上方列--%>

        <div class="main">
            <div class="header">
                <img src="/P/封面山.jpg" alt="Paris" width="1450" height="300" <%--style="width: 100%; height: 50%;"--%>>
            </div><%--封面山--%>

            <div class="row">
                <div class="leftcolumn">
                    <div class="card" style="background-color: #23eebc">
                        <h2>佈告欄</h2>
                        <div class="fakeimg" style="height: 200px;">
                            <p class="tm-margin-b-p">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" DataKeyNames="BID" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Department" ItemStyle-Width="20%" />
                                        <asp:TemplateField ItemStyle-Width="70%" ControlStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lb_Title" runat="server" Text='<%# Bind("BTitle") %>' CommandName="SelData"></asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Date" DataFormatString="{0:yyyy/MM/dd}" ItemStyle-Width="10%" />
                                        <asp:TemplateField ControlStyle-ForeColor="White">
                                            <FooterTemplate>
                                                <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="More..." />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT Top 10 * FROM [Bulletin] ORDER BY [BID] DESC"></asp:SqlDataSource>
                        </div>
                    </div>

                    <div class="card" style="background-color: #39f7f1; height: 350px;">
                        <h2>還要放什麼東西</h2>
                        <h5>20點</h5>
                        
                    </div>
                </div>

                <div class="rightcolumn">
                    <div class="card" style="background-color: #ef8b1e">
                        <h2>登入</h2>
                        <asp:Panel ID="Pel_Login" runat="server" Style=" padding: 10px; margin: auto;  width: auto; height: auto;" class="auto-style1">
                            <br />
                            <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" Width="265px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Width="265px"></asp:TextBox>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="登入" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Login_Click" />
                            &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="註冊" class="btn btn-info" Height="36px" Width="69px" OnClick="Button2_Click" />

                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="Pel_UserInfo" runat="server" Style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1" Visible="False">

                            <asp:Label ID="Lbl_Name" runat="server"></asp:Label>
                            &nbsp;您好
                                    <br />
                            員工編號：<asp:Label ID="Lbl_Eid" runat="server"></asp:Label>
                            <br />
                            部門/職稱：<asp:Label ID="Lbl_DpAndPos" runat="server"></asp:Label>
                        </asp:Panel>

                    </div>
                    <div class="card" style="background-color: #ab51fb">
                        <h3>連結?</h3>
                        <div class="fakeimg">Image</div>
                        <br>
                        <div class="fakeimg">Image</div>
                        <br>
                        <div class="fakeimg">Image</div>
                    </div>
                    <div class="card">
                        <p>關於</p>
                    </div>
                </div>
            </div><%--中間格--%>

            <div class="footer" style="background-color: #166ffb">
                <div class="row">
                    <div class="column" style="background-color: #166ffb;">
                        <h4>關於我們</h4>
                    </div>
                    <div class="column" style="background-color: #166ffb;">
                        <h4>常見問題</h4>
                    </div>
                    <div class="column" style="background-color: #166ffb;">
                        <h4>團隊成員</h4>
                    </div>
                     <div class="column" style="background-color: #166ffb;">
                        <h4>加入我們</h4>
                    </div>
                     <div class="column" style="background-color: #166ffb;">
                        <h4>隱私權聲明</h4>
                    </div>
                    <div class="column1" style="background-color: #166ffb;">
                        <h5>國立勤益科技大學National Chin-Yi University of Technology</h5>
                        <h5>地址：41170臺中市太平區坪林里中山路二段 57號</h5>
                        <h5>No.57, Sec. 2, Zhongshan Rd., Taiping Dist., Taichung 41170, Taiwan (R.O.C.)</h5>
                    </div>
                </div>
            </div><%--下方列--%>
        </div>
    </form>



</body>
</html>
