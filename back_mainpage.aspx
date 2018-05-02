﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="back_mainpage.aspx.cs" Inherits="WebApplication1.Back_mainpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/all.css" rel="stylesheet" />
    <div class="container-fluid">
        <div class="list">
            <div class="col-sm-2">
                <a href="Home.aspx" title="回首頁">
                    <div class="glyphicon glyphicon-home"></div>
                    <div class="item">回首頁</div>
                </a>
            </div>
            <div class="col-sm-2">
                 <a href="Home.aspx" title="公佈欄管理">
                    <div class="glyphicon glyphicon-th-list"></div>
                    <div class="item">公佈欄管理</div>
                </a>
            </div>
            <div class="col-sm-2">
                 <a href="Home.aspx" title="改變權限">
                    <div class="glyphicon glyphicon-resize-vertical"></div>
                    <div class="item">改變權限</div>
                </a>
            </div>
            <div class="col-sm-2">
                <a href="Home.aspx" title="新增">
                    <div class="glyphicon glyphicon-plus-sign   "></div>
                    <div class="item">新增</div>
                </a>
            </div>
             <div class="col-sm-2">
                <a href="Home.aspx" title="系統參數">
                    <div class="glyphicon glyphicon-wrench"></div>
                    <div class="item">系統參數</div>
                </a>
            </div>
        </div>
    </div>

</asp:Content>
