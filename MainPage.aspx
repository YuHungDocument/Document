<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebApplication1.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list a {
            display: block;
            text-align: center;
            text-decoration: none;
            padding: 16px;
            font-size: 14px;
            background-image: linear-gradient(to top, #00c6fb 0%, #005bea 100%);
            color: white;
        }

        .list-group a {
            display: block;
            text-decoration: none;
            padding: 16px;
            font-size: 14px;
        }

        .list-group li {
            border: 1px #b7b2b2 solid;
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
            <div class="col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="Home.aspx" title="回首頁" style="height: 152px; padding-top: 50px; font-size: large;"><span class="glyphicon glyphicon-home" style="font-size: xx-large;"></span>
                        <br />
                        回首頁</a>
                </div>
            </div>
            <div class="col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <div style="color: white; background-color: #66a6ff;">
                        <span class="glyphicon glyphicon-pencil" style="font-size: xx-large;"></span>
                        <br />
                        <div style="font-size: large">撰寫</div>
                    </div>
                    <a href="WriteDocument.aspx">公文</a>
                    <a href="WriteVote.aspx">投票</a>
                </div>
            </div>
            <div class=" col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="AllPage.aspx" title="所有公文及投票" style="height: 152px; padding-top: 50px; font-size: large;"><span class="glyphicon glyphicon-book" style="font-size: xx-large;"></span>
                        <br />
                        所有公文及投票</a>
                </div>
            </div>
            <div class=" col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="Draft.aspx" title="草稿" style="height: 152px; padding-top: 50px; font-size: large;"><span class="glyphicon glyphicon-edit" style="font-size: xx-large;"></span>
                        <br />
                        草稿</a>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="container-fluid">
        <div class="col-sm-3">
            <ul class="list-group" style="list-style: none;">
                <li class="list-group-item list-group-item-info">待處理項目</li>
                <li><a href="WaitDocument.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>待處理公文 <span class="badge" style="background-color: #C0C0C0;">
                    <asp:Label ID="Lbl_Doc" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Lbl_DocNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
                <li><a href="WaitVote.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>待處理投票 <span class="badge" style="background-color: #C0C0C0">
                    <asp:Label ID="Lbl_Vote" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Lbl_VoteNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
            </ul>

        </div>
        <div class="col-sm-3">
            <ul class="list-group" style="list-style: none;">
                <li class="list-group-item list-group-item-success">主辦項目</li>
                <li><a href="HostDocument.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>主辦公文 <span class="badge" style="background-color: #C0C0C0;">
                    <asp:Label ID="Lbl_HDoc" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Lbl_HDocNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
                <li><a href="HostVote.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>主辦投票 <span class="badge" style="background-color: #C0C0C0;">
                    <asp:Label ID="Lbl_Hvote" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Lbl_HVoteNew" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
            </ul>
        </div>
        <div class="col-sm-3">
            <ul class="list-group" style="list-style: none;">
                <li class="list-group-item list-group-item-success">已結案項目</li>
                <li><a href="WaitDocument.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>已結案公文 <span class="badge" style="background-color: #C0C0C0;">
                    <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Label2" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
                <li><a href="WaitVote.aspx"><i class="glyphicon glyphicon-play" style="color: #C0C0C0"></i>已結束投票 <span class="badge" style="background-color: #C0C0C0;">
                    <asp:Label ID="Label3" runat="server" Text="0"></asp:Label></span><span class="label label-danger"><asp:Label ID="Label4" runat="server" Text="New" Visible="False"></asp:Label></span></a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="list">
            <div class="col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="Home.aspx" title="標記重要項目" style="height: 152px; padding-top: 50px; font-size: large;"><span class="glyphicon glyphicon-star" style="font-size: xx-large;"></span>
                        <br />
                        標記重要項目</a>
                </div>

            </div>
            <div class="col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <div style="color: white; background-color: #66a6ff;">
                        <span class="glyphicon glyphicon-hdd" style="font-size: xx-large;"></span>
                        <br />
                        <div style="font-size: large">個人設定</div>
                    </div>
                    <a href="set.aspx">帳戶設定</a>
                    <a href="setInfo.aspx">個資設定</a>
                    <a href="SetAgent.aspx">代理人設定</a>
                    <a href="KeyAddress.aspx">金鑰設定</a>
                </div>
            </div>
            <div class=" col-sm-3" style="padding-top: 5px; text-align: center;">
                <div style="border: 3px solid #6699FF;">
                    <a href="SelectMemberData.aspx" title="後台管理" style="height: 152px; padding-top: 50px; font-size: large;"><span class="glyphicon glyphicon-edit" style="font-size: xx-large;"></span>
                        <br />
                        後台管理</a>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="Lbl_EID" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
