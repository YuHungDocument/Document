<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebApplication1.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list a {
            display: block;
            text-align: center;
            text-decoration: none;
            padding: 16px;
            font-size: x-large;
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
            <div class="col-sm-2" style="padding: 5px; text-align: center">
                <div style="border: 3px solid #6699FF;">
                    <a href="#" title="回主頁"><span class="glyphicon glyphicon-home"></span></a>
                </div>
            </div>

            <div class="col-sm-2" style="padding: 5px; text-align: center">
                <div style="border: 3px solid #6699FF;">
                    1
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

    </div>

</asp:Content>
