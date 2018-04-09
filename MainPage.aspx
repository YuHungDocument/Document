<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebApplication1.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list {
        }
        .list a {
            display: block;
            text-align: center;
            text-decoration: none;
            padding: 16px;
            font-size: 14px;
        }

            .list a:hover {
                color: white;
                background-color: #eea26b;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="list">
            <div class="col-sm-2" style="padding: 5px; text-align: center; ">
                <div style="border: 3px solid #6699FF;">
                    <a href="Home.aspx" title="回首頁"><span class="glyphicon glyphicon-home" style="font-size: xx-large"></span>
                        <br />
                        回首頁</a>
                </div>
            </div>
        </div>
        <div class="col-sm-2" style="padding: 5px; text-align: center; ">
            <div style="border: 3px solid #6699FF; height:90px">
                <span class="glyphicon glyphicon-pencil" style="font-size: xx-large"></span>
                <br />
                撰寫
                    <br />
                <div style="font-size: large">
                    <a href="WriteDocument.aspx">公文</a>|<a href="WriteVote.aspx">投票</a>
                 </div>
            </div>
        </div>

        <div class="col-sm-2" style="padding: 5px; text-align: center">
            <div style="border: 3px solid #6699FF;">
            </div>
        </div>

        <div class="col-sm-2" style="padding: 5px; text-align: center">
            <div style="border: 3px solid #6699FF;">
            </div>
        </div>

        <div class="col-sm-2" style="padding: 5px; text-align: center">
            <div style="border: 3px solid #6699FF;">
            </div>
        </div>

        <div class="col-sm-2" style="padding: 5px; text-align: center">
            <div style="border: 3px solid #6699FF;">
            </div>
        </div>


    </div>

</asp:Content>
