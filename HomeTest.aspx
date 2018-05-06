<%@ Page Title="" Language="C#" MasterPageFile="~/HomeTest.Master" AutoEventWireup="true" CodeBehind="HomeTest.aspx.cs" Inherits="WebApplication1.HomeTest1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                <img src="P/header-01.jpg" alt="Los Angeles" style="width: 100%;">
            </div>

            <div class="item">
                <img src="P/header-01.jpg" alt="Chicago" style="width: 100%;">
            </div>

            <div class="item">
                <img src="P/header-01.jpg" alt="New york" style="width: 100%;">
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
    <div id="12" style="background-color: #eaeaea">
        <div class="container" style="text-align: center">
            <h1>公文系統</h1>
        </div>
        <div class="container">
            <div class="col-sm-4">
                <h3><b>簡易的步驟</b></h3>
                <br />
                <h3><b>簡易的設定</b></h3>
                <br />
                <h3 style="color: gray"><b>寄出隱密的公文</b></h3>
            </div>
            <div class="col-sm-8" style="background-color:white; height:500px" >

            </div>
        </div>
    </div>
    <div id="13" style="background-color: ghostwhite">
        <div class="container" style="text-align: center">
            <h1>保密措施</h1>
        </div>
    <div class="container">
        <div class="col-lg-4">
            <asp:Image ID="Image1" runat="server" style="height:100px; max-width:100px" ImageUrl="~/P/rsa-red-logo.png"/>
        </div>
        <div class="col-lg-4"></div>
        <div class="col-lg-4"></div>
    </div>
    </div>
</asp:Content>
