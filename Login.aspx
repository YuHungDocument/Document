<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>
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
                        <asp:Panel ID="Pel_Login" runat="server" Style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">
                        登入
                                    <br />
                        <br />
                        <asp:TextBox class="form-control" placeholder="請輸入帳號" ID="Txt_ID" runat="server" Width="265px"></asp:TextBox>
                        <br />
                        <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Width="265px"></asp:TextBox>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="登入" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Login_Click" />
                        &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="註冊" class="btn btn-info" Height="36px" Width="69px" OnClick="Button2_Click" />

                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
                    </asp:Panel>
</asp:Content>
