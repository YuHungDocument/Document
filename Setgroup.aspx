<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Setgroup.aspx.cs" Inherits="WebApplication1.Setgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
                table{
            border-bottom:solid 1px #b7b2b2
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center;width:40%;height:40%;margin:0px auto;">
        <asp:GridView ID="GridView1" CssClass="table" runat="server" AutoGenerateColumns="False"  GridLines="Horizontal" BorderStyle="None" EmptyDataText="沒有任何群組" Font-Size="Large" OnRowCommand="GridView1_RowCommand" DataKeyNames="GID">
            <Columns>
                      <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_GID" runat="server" Text='<%#Eval("GID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="群組名稱">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_GpName" runat="server" Text='<%#Eval("GpName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="Lb_Edit" runat="server" CommandName="SelData">編輯</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="Lb_Del" runat="server" OnClientClick="return confirm('確定要刪除嗎？')" CommandName="DelData">刪除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
            <RowStyle HorizontalAlign="Left" />
            
        </asp:GridView>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
</asp:Content>
