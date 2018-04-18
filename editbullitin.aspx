<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="editbullitin.aspx.cs" Inherits="WebApplication1.editbullitin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="auto-style4">
                    <div class="container">
                    <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

                    <h3>公告設定<asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>
                        </h3>
                        <asp:GridView ID="GridView1" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                            GridLines="None" Width="100%" DataKeyNames="BID">
                            <Columns>
                                <asp:BoundField DataField="BID" HeaderText="BID" ReadOnly="True" SortExpression="BID" InsertVisible="False" />
                                <asp:TemplateField HeaderText="BTitle" SortExpression="BTitle">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BTitle") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                         <asp:LinkButton  Text='<%# Bind("BTitle") %>' ID="LinkButton1" runat="server" CommandName="SelData" CausesValidation="False"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                <asp:BoundField DataField="EID" HeaderText="EID" SortExpression="EID" />
                            </Columns>
                            <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
                <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
                <RowStyle Font-Size="Large" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [BID], [BTitle], [Department], [Date], [EID] FROM [Bulletin] WHERE (([EID] = @EID) AND ([EID] = @EID2))">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Lbl_EID" Name="EID" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="Lbl_EID" Name="EID2" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <asp:Button ID="Button1" class="btn btn-warning" runat="server" Text="新增公告" OnClick="Button1_Click" />
                </div>
                                            </div>
                </div>
</asp:Content>
