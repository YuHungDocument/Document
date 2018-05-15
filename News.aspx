<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="WebApplication1.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            border-color: #d7d7d7
        }

        table {
            border-color: white;
        }

            table tr td {
                height: 50px;
                line-height: 50px;
            }

            table tr a {
                display: block;
            }

            table tr:hover {
                background-color: #E1FFE1;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="Home.aspx">首頁</a>/最新消息
    <hr />
    <h3>最新消息</h3>
    <div style="text-align: right">
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    </div>
    <p></p>
    <div class="container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="NID" Width="100%" Font-Size="X-Large" GridLines="Horizontal" BackColor="White" AllowPaging="True" PageSize="5" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div style="text-align: center; background-color: #e9e9e9">公告類別</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center">
                            <asp:LinkButton ForeColor="Black" runat="server" Text='<%#Eval("NType")%>'></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div style="text-align: center; background-color: #e9e9e9">標題</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ForeColor="Black" runat="server" Text='<%#Eval("NTitle")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="150px">
                    <HeaderTemplate>
                        <div style="text-align: center; background-color: #e9e9e9">發布日期</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center">
                            <asp:LinkButton ForeColor="Blue" runat="server" Text='<%#Eval("Date","{0:yyyy/MM/dd}")%>'></asp:LinkButton>
                        </div>
                    </ItemTemplate>

                    <ItemStyle Width="150px"></ItemStyle>
                </asp:TemplateField>
            </Columns>

            <PagerTemplate>
                <div style="text-align: center; background-color: white;">
                    <ul class="pagination">
                        <li>
                            <asp:LinkButton ID="Lb_Frist" runat="server" CommandArgument="Frist" CommandName="page">第一頁</asp:LinkButton></li>
                                                <li>
                            <asp:LinkButton ID="Lb_Pre" runat="server" CommandArgument="Pre" CommandName="page">上一頁</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_01" runat="server" Text="1"  CommandArgument="One" CommandName="page"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_02" runat="server" Text="2"  CommandArgument="Two" CommandName="page"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_03" runat="server" Text="3"  CommandArgument="Third" CommandName="page"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_04" runat="server" Text="4"  CommandArgument="Four" CommandName="page"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_05" runat="server" Text="5"  CommandArgument="Five" CommandName="page"></asp:LinkButton></li>
                                                <li>
                            <asp:LinkButton ID="Lb_Next" runat="server" CommandArgument="Next" CommandName="page">下一頁</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="Lb_Last" runat="server" CommandArgument="Last" CommandName="page">最後一頁</asp:LinkButton></li>
                    </ul>
                </div>

            </PagerTemplate>

        </asp:GridView>
    </div>

</asp:Content>
