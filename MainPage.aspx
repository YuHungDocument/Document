<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebApplication1.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list a {
            display: block;
            text-align: center;
            text-decoration: none;
            padding: 16px;
            font-size: 14px;
            background-color: #81a9f8;
            color: white;
        }

        .list-group a {
            display: block;
            text-decoration: none;
            padding: 16px;
            font-size: 14px;
            color:white;
            background-color:#14b9f7;
        }

            .list-group a:hover {
                color: white;
                background-color: #eea26b;
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
            <div class="col-sm-2" style="padding: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="Home.aspx" title="回首頁" style="height: 152px; padding-top: 50px"><span class="glyphicon glyphicon-home" style="font-size: xx-large;"></span>
                        <br />
                        回首頁</a>
                </div>
            </div>
            <div class="col-sm-2" style="padding: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <div style="color: white; background-color: #6699FF;">
                        <span class="glyphicon glyphicon-pencil" style="font-size: xx-large;"></span>
                        <br />
                        撰寫
                    </div>
                    <div style="font-size: large">
                        <a href="WriteDocument.aspx">公文</a>
                        <a href="WriteVote.aspx">投票</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div >
        <div class="col-sm-6">
            <ul class="list-group" style="list-style: none; width:50%">
                <li class="list-group-item list-group-item-success">待處理項目</li>
                <li><a href="#">待處理公文 <span class="badge" style="background-color: #FFFFFF;color:black; "><asp:Label ID="Lbl_Doc" runat="server" Text="0"></asp:Label></span> <span class="label label-danger"><asp:Label ID="Lbl_DocNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
                <li><a href="#">待處理投票 <span class="badge" style="background-color: #FFFFFF;color:black;  "><asp:Label ID="Lbl_Vote" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Lbl_VoteNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
            </ul>
        </div>
    </div>
    <asp:Label ID="Lbl_EID" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
