<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--背景圖避免分身--%>
    <style>
        html {
            height: 100%;
        }

        body {
            background-image: url(background.jpg);
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
            background-size: cover;
        }

        p.groove {
            border-style: groove;
        }
    </style>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <%--九宮格--%>
    <style>
        * {
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }



        /* Create three unequal columns that floats next to each other */
        .column {
            float: left;
            padding: 10px;
            height: 650px; /* Should be removed. Only for demonstration */
        }

        .column0 {
            float: left;
            padding: 10px;
            height: 120px; /* Should be removed. Only for demonstration */
        }

        .column1 {
            float: left;
            padding: 10px;
            height: 200px; /* Should be removed. Only for demonstration */
        }

        /* Left and right column */
        .column.side {
            width: 20%;
        }

        /* Middle column */
        .column.middle {
            width: 60%;
        }

        .column0.side {
            width: 20%;
        }

        .column0.middle {
            width: 60%;
        }

        .column1.side {
            width: 20%;
        }

        .column1.middle {
            width: 60%;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }



        /* Responsive layout - makes the three columns stack on top of each other instead of next to each other */
        @media (max-width: 600px) {
            .column.side, .column.middle {
                width: 100%;
            }
        }
    </style>

    <%--中央分隔用--%>
    <style>
        * {
            box-sizing: border-box;
        }

        /* Create two equal columns that floats next to each other */
        .column4 {
            float: left;
            width: 50%;
            padding: 10px;
            height: 300px; /* Should be removed. Only for demonstration */
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        .auto-style1 {
            float: left;
            width: 34%;
            padding: 10px;
            height: 252px; /* Should be removed. Only for demonstration */
        }
        .auto-style2 {
            width: 92px;
        }
        .auto-style3 {
            width: 222px;
        }
        .auto-style4 {
            float: left;
            width: 55%;
            padding: 10px;
            height: 300px; /* Should be removed. Only for demonstration */
        }
    </style>

</head>

<body>
    <form id="form2" runat="server">
        <%--上面排--%>
        <div class="row">
            <div class="column0 side" style="background-color: #6c6969;"></div>
            <div class="column0 middle" style="background-color: #8AABF7">
                <h2>電子公文暨投票系統</h2>                
            </div>
            <div class="column0 side" style="background-color: #6c6969;"></div>
        </div>

        <%--中間排--%>
        <div class="row">

            <div class="column side" style="background-color: #6c6969;"></div>
            <div class="column middle" style="background-color: #FFFFFF;">

                <br />
                <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

                    <h3>登入</h3>
                    <br />
                    <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" Width="265px"></asp:TextBox>
                    <br />
                    <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Width="265px"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="登入" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Login_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="註冊" class="btn btn-info" Height="36px" Width="69px" OnClick="Button2_Click"  />
                    
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
                </div>
                <div class="auto-style4">
                    <div class="container">
                        <h3>電子佈告欄</h3>                        
                        <table class="table table-striped" style="width: 95%">
                            <thead>
                                <tr>
                                    <th class="auto-style2">發佈時間</th>
                                    <th class="auto-style3">標題</th>
                                    <th>公告單位</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="auto-style2">2000.00.00</td>
                                    <td class="auto-style3">成本控制</td>
                                    <td>財務部</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">2000.00.00</td>
                                    <td class="auto-style3">業績下滑10%</td>
                                    <td>銷售部</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">2000.00.00</td>
                                    <td class="auto-style3">尾牙晚會</td>
                                    <td>人資部</td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">2000.00.00</td>
                                    <td class="auto-style3">春節旅遊</td>
                                    <td>業務部</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                </div>


            </div>
            <div class="column side" style="background-color: #6c6969;"></div>
        </div>

        <%--下面排--%>
        <div class="row">
            <div class="column1 side" style="background-color: #6c6969;"></div>
            <div class="column1 middle" style="color: #FFFFFF; background-color: #484646;">
                <h5>國立勤益科技大學資管專題小組</h5>
                <h5>地址：41170臺中市太平區坪林里中山路二段 57號</h5>
                    <h5></h5>
                    <h5></h5>
            </div>
            <div class="column1 side" style="background-color: #6c6969;"></div>
        </div>
    </form>









</body>
</html>
