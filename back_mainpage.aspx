<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Back_mainpage.aspx.cs" Inherits="WebApplication1.Back_mainpage" %>
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
                 <a href="editbullitin.aspx" title="公佈欄管理">
                    <div class="glyphicon glyphicon-th-list"></div>
                    <div class="item">公佈欄管理</div>
                </a>
            </div>
            <div class="col-sm-2">
                 <a href="SelectMemberData.aspx" title="改變權限">
                    <div class="glyphicon glyphicon-resize-vertical"></div>
                    <div class="item">改變權限</div>
                </a>
            </div>
            <div class="col-sm-2">
                <a href="Edit_U.aspx" title="新增">
                    <div class="glyphicon glyphicon-plus-sign   "></div>
                    <div class="item">編輯</div>
                </a>
            </div>
             <div class="col-sm-2">
                <a href="Register.aspx" title="新帳戶註冊">
                    <div class="glyphicon glyphicon-wrench"></div>
                    <div class="item">新帳戶註冊</div>
                </a>
            </div>
            <div class="col-sm-2">
                <a href="Remove_employee.aspx" title="管理離職員工">
                    <div class="glyphicon glyphicon-wrench"></div>
                    <div class="item">管理離職員工</div>
                </a>
            </div>
        </div>
    </div>

</asp:Content>
