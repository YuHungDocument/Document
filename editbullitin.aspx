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
                        <div style="text-align: right">
                    <asp:DropDownList ID="Ddl_Type" class="form-control" Height="50px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddl_Type_SelectedIndexChanged" Font-Size="X-Large" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN">
                    </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Dp" Name="Tp" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                </div>
                <p></p>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="BID" Width="100%" Font-Size="X-Large" GridLines="Horizontal" BackColor="White" AllowPaging="True" PageSize="5" OnDataBound="GridView1_DataBound" OnPreRender="GridView1_PreRender" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div style="text-align: center; background-color: #e9e9e9">公告部門</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton ForeColor="Black" runat="server" CommandName="SelData" Text='<%#Eval("Dp")%>'></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div style="text-align: center; background-color: #e9e9e9">標題</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                <asp:LinkButton ForeColor="Black" runat="server" CommandName="SelData" Text='<%#Eval("BTitle")%>'></asp:LinkButton>
                                     </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="150px">
                            <HeaderTemplate>
                                <div style="text-align: center; background-color: #e9e9e9">發布日期</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton ForeColor="Blue" runat="server" CommandName="SelData" Text='<%#Eval("Date","{0:yyyy/MM/dd}")%>'></asp:LinkButton>
                                </div>
                            </ItemTemplate>

                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>

                    <PagerSettings Position="Top" />

                    <PagerStyle BorderStyle="None" BorderColor="White" />

                    <PagerTemplate>
                        <div style="text-align: right; background-color: white">
                            第<asp:DropDownList ID="Ddl_Page" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddl_Page_SelectedIndexChanged"></asp:DropDownList>頁<asp:Label ID="Lbl_View" runat="server" Text="Label"></asp:Label>
                        </div>
                    </PagerTemplate>

                </asp:GridView>
                        <br />
                        <asp:Button ID="Button1" class="btn btn-warning" runat="server" Text="新增公告" OnClick="Button1_Click" />
                </div>
                                            </div>
                </div>
</asp:Content>
