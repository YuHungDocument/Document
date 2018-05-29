<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="WebApplication1.NewsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr td {
            text-align: center;
            height: 50px;
            line-height: 50px;
        }

        .btn {
            border: solid 1px #00b465;
            background-color: white;
            color: #00b465;
        }

            .btn:hover {
                border: solid 1px #ffffff;
                background-color: #00b465;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="text-align: center">
        <asp:Button CssClass="btn" ID="Btn_Insert" runat="server" Text="新增消息" OnClick="Btn_Insert_Click" /></div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" ShowHeader="false" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="NID" BackColor="White" BorderStyle="None" BorderColor="#CCCCCC">
        <Columns>
            <asp:BoundField DataField="Date" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="120px">
                <ItemStyle ForeColor="Orange" />
            </asp:BoundField>
            <asp:BoundField DataField="NTitle" HeaderText="標題">
                <ItemStyle ForeColor="Black" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="編輯">
                <ItemTemplate>
                    <asp:LinkButton ID="Lb_Edit" runat="server" CommandName="SelData">編輯</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="刪除">
                <ItemTemplate>
                    <asp:LinkButton ID="Lb_Del" runat="server" onclientclick="return confirm('確定要刪除嗎？')" CommandName="DelData">刪除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BorderStyle="None" />
        <HeaderStyle BorderStyle="None" />
        <RowStyle BorderStyle="None" Font-Size="Large" />
    </asp:GridView>
</asp:Content>
