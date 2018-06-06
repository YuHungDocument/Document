<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Setgroup.aspx.cs" Inherits="WebApplication1.Setgroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="false">
        <Columns>
            <asp:BoundField DataField="GID" Visible="false"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="Lb_Name" runat="server" Text='<%#Eval("GpName")%>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
</asp:Content>
