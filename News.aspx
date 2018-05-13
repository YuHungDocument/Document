<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="WebApplication1.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            border-bottom: 1px solid #c3c3c3;
            
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
    <br />
    <div class="container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  BorderStyle="None" DataKeyNames="NID" Width="100%" Font-Size="X-Large" GridLines="Horizontal">
            <Columns>
                <asp:TemplateField  >
                    <HeaderTemplate>
                        <div style="text-align:center; background-color:#4dd902">標題</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ForeColor="Black" runat="server" Text='<%#Eval("NTitle")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="150px" >
                                        <HeaderTemplate>
                        <div style="text-align:center; background-color:#4dd902">發布日期</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:center"><asp:LinkButton ForeColor="Blue" runat="server" Text='<%#Eval("Date","{0:yyyy/MM/dd}")%>'></asp:LinkButton></div>
                    </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
    </div>

</asp:Content>
