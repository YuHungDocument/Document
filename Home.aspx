<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @-webkit-keyframes TestMove {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        #serect {
            height: 400px;
        }

        .thumbnail {
            padding: 0 0 15px 0;
            border: solid 1px #ccc;
            border-radius: 0;
        }

            .thumbnail p {
                margin-top: 15px;
                color: #555;
            }

            .thumbnail strong {
                font-size: large;
            }

        .circle_in_black {
            width: 80px;
            height: 80px;
            border-radius: 99em;
            border: solid 1px #808080;
        }

            .circle_in_black:hover {
                background-color: orange;
                border: none;
                color: white;
            }

        table tr td {
            text-align: center;
            height: 50px;
            line-height: 50px;
        }

            table tr td a {
                display: block;
                color:#555
            }

            table tr td span {
                padding-top: 23px;
                color: #555;
                font-size: xx-large;
            }

            table tr td a:hover > span {
                background-color: orange;
                border: none;
                color: white;
            }

            table tr td a:hover {
                color: orange;
                text-decoration: none;
            }
    </style>

    <script> 

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="2"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
            <div class="item active">
                <img src="P/header-01.jpg" alt="Los Angeles" style="height: 300px; width: 100%;">
            </div>

            <div class="item">
                <img src="P/header-01.jpg" alt="Chicago" style="height: 300px; width: 100%;">
            </div>

            <div class="item">
                <img src="P/header-01.jpg" alt="New york" style="height: 300px; width: 100%;">
            </div>
        </div>

        <!-- Left and right controls -->
        <a class="left carousel-control" href="#myCarousel" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
    <br />
    <div id="serect">
        <div class="col-md-6 col-sm-12">
            <h3>News|最新消息</h3>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" ShowHeader="False" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="NID" BackColor="White" BorderStyle="None" BorderColor="#CCCCCC">
                <Columns>
                    <asp:BoundField DataField="Date" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="120px">
                        <ItemStyle ForeColor="Orange" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton Style="text-align: left; padding-top: 0px;" ID="Lb_Title" runat="server" Text='<%# Bind("NTitle") %>' CommandName="SelData"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle ForeColor="Black" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderStyle="None" />
                <HeaderStyle BorderStyle="None" />
                <RowStyle BorderStyle="None" Font-Size="Large" />
            </asp:GridView>
            <br />
            <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="更多資訊" OnClick="Button3_Click" />
        </div>
        <div class="col-md-6 col-sm-12">

            <table style="margin-top:50px" class="nav-justified">
                <tr>
                    <td><a href="Register.aspx"><span class="circle_in_black glyphicon glyphicon-hand-right"></span>
                        <br />
                        加入我們</a></td>
                    <td><a href="#"><span class="circle_in_black glyphicon glyphicon-th-list"></span>
                        <br />
                        網頁導覽</a></td>
                    <td><a href="#"><span class="circle_in_black glyphicon glyphicon-leaf"></span>
                        <br />
                        服務專區</a></td>
                </tr>
                <tr>
                    <td><a href="MainPage.aspx"><span class="circle_in_black glyphicon glyphicon-briefcase"></span>
                        <br />
                        公文系統</a></td>
                    <td><a href="AboutUs.aspx"><span class="circle_in_black glyphicon glyphicon glyphicon-pencil"></span>
                        <br />
                        關於我們</a></td>
                    <td><a href="Login.aspx"><span class="circle_in_black glyphicon glyphicon-user"></span>
                        <br />
                        員工專區</a></td>
                </tr>
            </table>

        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ff0000">
                <asp:Image ID="Image1" runat="server" Width="400" Height="300" ImageUrl="~/P/document.png" />
                <p><strong>公文系統</strong></p>
                <p>簡易的公文系統，前往立即使用</p>
                <div style="text-align:center;"><asp:Button class="button" ID="Button1"  runat="server" Text="前往了解" PostBackUrl="~/MainPage.aspx" OnClick="Button1_Click" /></div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ff6a00">
                <asp:Image ID="Image2" runat="server" Width="400" Height="300" ImageUrl="~/P/aboutus.png" />
                <p><strong>關於我們</strong></p>
                <p>有關我們的資訊，都可以從這裡去了解</p>
                <div style="text-align:center;"><asp:Button ID="Button2" class="button" runat="server" Text="前往了解" OnClick="Button2_Click" /></div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ffd800">
                <asp:Image ID="Image3" runat="server" Width="400" Height="300" ImageUrl="~/P/usedocument.png" />
                <p><strong>操作方式</strong></p>
                <p>有什麼不懂得來這裡看就對了</p>
                <div style="text-align:center;"><asp:Button ID="Button4" class="button" runat="server" Text="前往了解" /></div>
            </div>
        </div>
    </div>
    <br />


</asp:Content>
