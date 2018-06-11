<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="editbullitin.aspx.cs" Inherits="WebApplication1.editbullitin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            border-color: #d7d7d7
        }



            table tr td {
                height: 50px;
                line-height: 50px;
            }

                table tr td a {
                    display: block;
                }

            table tr:hover {
                background-color: #E1FFE1;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style4">
        <div class="container">
            <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

                <h3>公告設定<asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>
                </h3>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" ShowHeader="false" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="BID" BackColor="White" BorderStyle="None" BorderColor="#CCCCCC">
                    <Columns>
                        <asp:BoundField DataField="Date" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="120px">
                            <ItemStyle ForeColor="Orange" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BTitle" HeaderText="標題">
                            <ItemStyle ForeColor="Black" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lb_Edit" runat="server" CommandName="SelData">編輯</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:LinkButton ID="Lb_Del" runat="server" OnClientClick="return confirm('確定要刪除嗎？')" CommandName="DelData">刪除</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BorderStyle="None" />
                    <HeaderStyle BorderStyle="None" />
                    <RowStyle BorderStyle="None" Font-Size="Large" />
                </asp:GridView>
                <br />
                <asp:Button ID="Button1" class="btn btn-warning" runat="server" Text="新增公告" OnClick="Button1_Click" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="返回後臺" class="btn btn-warning" OnClick="Button2_Click" />

                </div>
            </div>
        </div>
</asp:Content>
