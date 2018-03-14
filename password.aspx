<%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="password.aspx.cs" Inherits="WebApplication1.password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="auto-style4">
                    <div class="container">
                    <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

                    <h3>確認目前密碼</h3>
                    <br />
                        <asp:Label ID="Lbl_ID" runat="server" class="badge badge-pill badge-light" Font-Size="Larger" Height="19px" Width="150px" ></asp:Label>
                        <br />
                    <br />
                    <asp:TextBox class="form-control" placeholder="請輸入密碼" ID="Txt_Password" runat="server" TextMode="Password" Width="265px"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="下一步" class="btn btn-warning" Height="36px" Width="69px" OnClick="Btn_Next_Click" />
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="密碼錯誤，再輸入一次" Visible="False"></asp:Label>
                </div>
                                            </div>


                </div>
 </asp:Content>
