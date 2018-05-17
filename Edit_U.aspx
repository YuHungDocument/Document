<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Edit_U.aspx.cs" Inherits="WebApplication1.Edit_U" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/all.css" rel="stylesheet" />
    <div class="panel">
        <div class="panel panel-primary">
        <div class="panel-heading pull clickable">
            <h3 class="panel-title "><span>新增<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td class="auto-style1">
                        <div class="panel panel-default" <%--style="width: 500px"--%>>
                            <div class="panel-heading">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Value="Dp">部門</asp:ListItem>
                                    <asp:ListItem Value="FT">公文總類</asp:ListItem>
                                    <asp:ListItem Value="PO">職稱</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

                                <asp:Button ID="Button2" runat="server" Text="確定新增" OnClick="Button2_Click" />

                                <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TextBox4" runat="server" Visible="False"></asp:TextBox>
                            </div>

                            <div class="panel-body">
                                <asp:GridView ID="TG" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_TG" runat="server" Text='<%# Eval("TN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" BorderWidth="0px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
            </div>
    </div>
        
</asp:Content>
