<%@ Page Title="" Language="C#" MasterPageFile="~/HomeTest.Master" AutoEventWireup="true" CodeBehind="HomeTest.aspx.cs" Inherits="WebApplication1.HomeTest1" %>

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
                            <asp:LinkButton ID="Lb_Title" runat="server"  Text='<%# Bind("NTitle") %>' CommandName="SelData"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="Lb_space" runat="server" CommandName="SelData"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle ForeColor="Black" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderStyle="None" />
                <HeaderStyle BorderStyle="None" />
                <RowStyle Height="40px" BorderStyle="None" Font-Size="Large" />
            </asp:GridView>
            <br />
            <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="更多資訊" />
        </div>
        <div class="col-md-6 col-sm-12">
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ff0000">
                <asp:Image ID="Image1" runat="server" Width="400" Height="300" ImageUrl="~/P/document.jpeg" />
                <p><strong>公文系統</strong></p>
                <p>簡易的公文系統，前往立即使用</p>
                <asp:Button ID="Button1" class="btn" runat="server" Text="前往了解" />
            </div>
        </div>
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ff6a00">
                <asp:Image ID="Image2" runat="server" Width="400" Height="300" ImageUrl="~/P/aboutus.png" />
                <p><strong>關於我們</strong></p>
                <p>有關我們的資訊，都可以從這裡去了解</p>
                <asp:Button ID="Button2" class="btn" runat="server" Text="前往了解" />
            </div>
        </div>
        <div class="col-sm-4">
            <div class="thumbnail" style="border-top: 5px solid #ffd800">
                <asp:Image ID="Image3" runat="server" Width="400" Height="300" ImageUrl="~/P/usedocument.png" />
                <p><strong>操作方式</strong></p>
                <p>有什麼不懂得來這裡看就對了</p>
                <asp:Button ID="Button4" class="btn" runat="server" Text="前往了解" />
            </div>
        </div>
    </div>
    <br />


</asp:Content>
