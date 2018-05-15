<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="WebApplication1.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            border-color: #d7d7d7
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
    <h2>最新消息</h2>

    <p></p>
    <div class="container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="NID" Width="100%" Font-Size="X-Large" GridLines="Horizontal" BackColor="White" AllowPaging="True" PageSize="5" OnDataBound="GridView1_DataBound"  >
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

            <PagerSettings Position="Top" />

            <PagerStyle BorderStyle="None" BorderColor="White" />

            <PagerTemplate>
                <div class="col-sm-6" style=" background-color:white">
                    <asp:DropDownList ID="Ddl_Type" runat="server" AutoPostBack="True">
                        <asp:ListItem>全部類別</asp:ListItem>
                        <asp:ListItem>新聞發布</asp:ListItem>
                        <asp:ListItem>系統公告</asp:ListItem>
                        <asp:ListItem>更新消息</asp:ListItem>
                        <asp:ListItem>其他訊息</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; 切換頁數<asp:DropDownList ID="Ddl_Page" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddl_Page_SelectedIndexChanged"></asp:DropDownList>
                    
                </div>
                
                <div class="col-sm-6" style="text-align:right;background-color:white"><asp:Label ID="Lbl_View" runat="server" Text="Label"></asp:Label></div>
                <p></p>
            </PagerTemplate>

        </asp:GridView>
    </div>

</asp:Content>
