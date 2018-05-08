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
            height: 500px;
        }

        .thumbnail {
            padding: 0 0 15px 0;
            border: none;
            border-radius: 0;
        }

            .thumbnail p {
                margin-top: 15px;
                color: #555;
            }

            .thumbnail strong {
                font-size: large;
            }

        .bg-1 {
            background-color: #2d2d30;
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
    <div class="bg-1">
        <div class="row">
            <div class="col-sm-4">
                <div class="thumbnail">
                    <asp:Image ID="Image1" runat="server" Width="400" Height="300" ImageUrl="~/P/document.jpeg" />
                    <p><strong>公文系統</strong></p>
                    <p>簡易的公文系統，前往立即使用</p>
                    <asp:Button ID="Button1" class="btn" runat="server" Text="前往" />
                </div>
            </div>
            <div class="col-sm-4">
                <div class="thumbnail">
                    <asp:Image ID="Image2" runat="server" Width="400" Height="300" ImageUrl="~/P/document.jpeg" />
                    <p><strong>New York</strong></p>
                    <p>Saturday 28 November 2015</p>
                    <button class="btn" data-toggle="modal" data-target="#myModal">Buy Tickets</button>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="thumbnail">
                    <asp:Image ID="Image3" runat="server" Width="400" Height="300" ImageUrl="~/P/document.jpeg" />
                    <p><strong>New York</strong></p>
                    <p>Saturday 28 November 2015</p>
                    <button class="btn" data-toggle="modal" data-target="#myModal">Buy Tickets</button>
                </div>
            </div>
        </div>

    </div>
    <br />
    <div id="serect">
        <div class="col-md-6 col-sm-12">
            <h3>News|最新消息</h3>
            <hr />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="false" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" OnRowCommand="GridView1_RowCommand" DataKeyNames="BID" BackColor="White">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="Lb_Title" runat="server" Text='<%# Bind("bull") %>' CommandName="SelData"></asp:LinkButton>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:LinkButton ID="Lb_Date" runat="server" Text='<%# Bind("ConDate") %>' CommandName="SelData"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="More..." />
                        </FooterTemplate>

                        <FooterStyle HorizontalAlign="Right"></FooterStyle>

                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <RowStyle Height="30px" />
            </asp:GridView>
        </div>
        <div class="col-md-6 col-sm-12">
        </div>

    </div>

</asp:Content>
