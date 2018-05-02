<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="row tm-item-preview tm-margin-b-xl">
        <div class="auto-style2">
            <h2 class="tm-blue-text tm-margin-b-p">我們團隊</h2>
        </div>
        <div class="col-md-6 col-sm-12 mb-md-0 mb-5">
            <div class="mr-lg-5">
                <p class="tm-margin-b-p">
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
                    <asp:Panel ID="Pel_UserInfo" runat="server" Style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1" Visible="False">

                        <asp:Label ID="Lbl_Name" runat="server"></asp:Label>
                        &nbsp;您好
                                    <br />
                        員工編號：<asp:Label ID="Lbl_Eid" runat="server"></asp:Label>
                        <br />
                        部門/職稱：<asp:Label ID="Lbl_DpAndPos" runat="server"></asp:Label>
                    </asp:Panel>
                </p>
                <br />
            </div>
        </div>
        <div class="col-md-6 col-sm-12 tm-highlight tm-small-pad">
            <h2 class="tm-margin-b-p">佈告欄</h2>
            <hr />
            <p class="tm-margin-b-p">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" OnRowCommand="GridView1_RowCommand" DataKeyNames="BID">
                    <Columns>
                        <asp:TemplateField ControlStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lb_Title" runat="server" Text='<%# Bind("bull") %>' CommandName="SelData"></asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lb_Date" runat="server" Text='<%# Bind("ConDate") %>' CommandName="SelData"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-ForeColor="White">
                            <FooterTemplate>
                                <asp:Button ID="Button3" class="btn btn-info" runat="server" Text="More..." />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </p>
        </div>
    </section>


    <!-- load JS files -->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script>     

        $(document).ready(function () {

            // Update the current year in copyright
            $('.tm-current-year').text(new Date().getFullYear());

        });

    </script>
</asp:Content>
