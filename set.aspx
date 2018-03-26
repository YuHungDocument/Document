<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="set.aspx.cs" Inherits="WebApplication1.set" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        * {
            box-sizing: border-box;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }



        /* Create three equal columns that floats next to each other */
        .column {
            float: left;
            width: 60%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column1 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        .column2 {
            float: left;
            width: 20%;
            padding: 10px;
            height: 1000px; /* Should be removed. Only for demonstration */
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        /* Style the footer */
        .footer {
            background-color: #f1f1f1;
            padding: 10px;
            text-align: center;
        }

        /* Responsive layout - makes the three columns stack on top of each other instead of next to each other */
        @media (max-width: 600px) {
            .column {
                width: 100%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="column1" style="">
        </div>
        <div class="column" style="">
            <div class="qVrTkf ZhDf7c">
                <div class="LTC23e">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/P/鑰匙.png" Height="143px" Width="219px" />
                    <div class="rdUUMc">密碼和帳戶登入方式</div>
                    <div class="H7P8yb">密碼是帳戶的第一道屏障。<br>
                        <br>
                        <b>注意：</b>您必須先確認密碼，才能變更這些設定。
                    </div>
                </div>
                <div class="K0zRed">
                    <a href="password.aspx" class="bsFZu">
                        <div class="yzO83e ">
                            <div class="K9yn1e"></div>
                            <div class="JsdkBf">
                                <div class="dLswc">
                                    <div class="cCS6xc">
                                        <h5 class="rdUUMc">變更密碼</h5>
                                    </div>
                                    <div class="oJFOKe">
                                        <asp:Label ID="Lbl_ChangePwdTime" runat="server" Text="上次變更時間："></asp:Label>

                                    </div>
                                </div>
                                <div class="Phkdhd"></div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <div class="column2" style="">
        </div>
    </div>
</asp:Content>
