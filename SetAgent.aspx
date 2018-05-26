<%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SetAgent.aspx.cs" Inherits="WebApplication1.SetAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

            <h3>代理人資訊</h3>
            <asp:GridView ID="GridView1" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" DataKeyNames="agent,EID" DataSourceID="SqlDataSource1"
                GridLines="None" Width="100%">
                <Columns>
                    <asp:BoundField DataField="agent" HeaderText="代理人編號" ReadOnly="True" SortExpression="agent" />
                    <asp:BoundField DataField="EID" HeaderText="被代理人編號" ReadOnly="True" SortExpression="EID" />
                    <asp:BoundField DataField="AgentName" HeaderText="代理人姓名" SortExpression="AgentName" />
                    <asp:BoundField DataField="StartTime" HeaderText="開始代理日期" SortExpression="StartTime" />
                    <asp:BoundField DataField="EndTime" HeaderText="結束代理日期" SortExpression="EndTime" />
                    <asp:BoundField DataField="send" HeaderText="可否發文" SortExpression="send" />
                    <asp:BoundField DataField="receive" HeaderText="可否收文" SortExpression="receive" />
                </Columns>
                <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
                <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
                <RowStyle Font-Size="Large" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [AgentInfo]"></asp:SqlDataSource>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" class="btn btn-warning" runat="server" Text="代理人調整" OnClick="Button1_Click" />
        </div>

    </div>
</asp:Content>
