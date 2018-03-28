<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Draft.aspx.cs" Inherits="WebApplication1.Draft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        每頁顯示<asp:TextBox ID="TxtPageSize" runat="server" Height="16px" TextMode="Number" Width="39px"></asp:TextBox>
        筆資料
            <asp:Button ID="Change" runat="server" CausesValidation="False" class="btn btn-default" OnClick="Change_Click" Text="更改" />
    </div>
    <asp:GridView ID="Menu" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" DataKeyNames="SID" OnRowCommand="GridView1_RowCommand" AllowSorting="True"  GridLines="None" Width="100%" EmptyDataText="暫無待處理項目"
        AllowPaging="True" OnRowCreated="Gridview_OnRowCreated"
        OnSorting="Gv_Sorting" PageSize="3" OnPageIndexChanging="gv_PageIndexChanging">
        <FooterStyle ForeColor="Black" />
        <PagerStyle HorizontalAlign="Center" />
        <SortedAscendingHeaderStyle CssClass="asc" />
        <SortedDescendingHeaderStyle CssClass="dsc" />
        <Columns>
            <asp:TemplateField HeaderText="類別">
                <ItemTemplate>
                    <asp:Label Text='<%#Eval("Type")%>' ID="Lbl_Type" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="主旨" SortExpression="Title">
                <ItemTemplate>
                    <asp:LinkButton data-toggle="tooltip" Text='<%#Eval("Title")%>' ID="Lb_SID" runat="server" CommandName="SelData" CausesValidation="False"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SID" Visible="False">
                <ItemTemplate>
                    <asp:Label Text='<%#Eval("SID")%>' ID="Lbl_SID" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
        <HeaderStyle Font-Size="Large" />
        <RowStyle Font-Size="Large" BackColor="White" />
    </asp:GridView>
    <div>
        每頁
            <asp:Label ID="lbl_1" runat="server" Text=""></asp:Label>
        筆 / 共
            <asp:Label ID="lbl_2" runat="server" Text=""></asp:Label>
        筆 / 第
            <asp:Label ID="lbl_3" runat="server" Text=""></asp:Label>
        頁 / 共
            <asp:Label ID="lbl_4" runat="server" Text=""></asp:Label>
        頁
     
        <asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>
