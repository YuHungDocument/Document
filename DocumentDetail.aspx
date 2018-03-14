<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.Master" AutoEventWireup="true" CodeBehind="DocumentDetail.aspx.cs" Inherits="WebApplication1.DocumentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="col-sm-8">
            <asp:Label ID="Lbl_Title" runat="server" Font-Size="X-Large" Font-Names="標楷體"></asp:Label>
            &nbsp;
            <asp:Label ID="Lbl_SID" runat="server" ForeColor="#999999"></asp:Label>
            <asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>


            &nbsp;
            <asp:Label ID="Lbl_Type" runat="server" BackColor="#FFD5AA" Font-Names="標楷體" Font-Size="Medium"></asp:Label>


        </div>
        <div class="col-sm-4" style="text-align: right">
            發布日期：<asp:Label ID="Lbl_Date" runat="server" Text="Label"></asp:Label>
        </div>
        <br />
        <hr />
        <div class="col-sm-12">
            <asp:Label ID="Lbl_Text" runat="server"></asp:Label>
        </div>
        <br />
        <hr />
        <div class="col-sm-12">
            擬辦：
            <br />
            <asp:Label ID="Lbl_Proposition" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Pel_Choose" runat="server" Visible="False">
                您的投票選項為：<asp:Label ID="Lbl_Choose" runat="server"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="Pel_selectwatch" runat="server" Visible="False">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" GridLines="None" Width="148px">
                    <Columns>
                        <asp:BoundField DataField="number" HeaderText="選項" />
                        <asp:BoundField DataField="Vname" />
                    </Columns>
                </asp:GridView>
                <br />
                選擇選項：<asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </asp:Panel>
        </div>

    </div>
</asp:Content>
