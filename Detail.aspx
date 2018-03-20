<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApplication1.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="col-sm-8">
        <asp:Label ID="Lbl_Title" runat="server" Font-Size="X-Large" Font-Names="標楷體"></asp:Label>
        &nbsp;
            <label for="male" style="color: #999999">文號：</label><asp:Label ID="Lbl_SID" runat="server" ForeColor="#999999"></asp:Label>
        <asp:Label ID="Lbl_EID" runat="server" Visible="False"></asp:Label>
        &nbsp;主辦人文號:
        <asp:Label ID="Lbl_SenderEID" runat="server"></asp:Label>
        &nbsp;
        <asp:Label ID="Lbl_SenderName" runat="server"></asp:Label>
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
        <asp:GridView ID="gv_showTempFile" runat="server" AutoGenerateColumns="false" DataKeyNames="FNO">
            <Columns>
                <asp:TemplateField HeaderText="已上傳的檔案">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_filename" runat="server" OnClick="OpenDoc" Text='<%# Eval("Name") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Panel ID="Pnl_sign" runat="server" Visible="False">
            <asp:Panel ID="Pel_selectwatch" runat="server" Visible="False">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" GridLines="None" Width="148px">
                    <Columns>
                        <asp:BoundField DataField="number" />
                        <asp:BoundField DataField="Vname" />
                    </Columns>
                </asp:GridView>
                <br />
                選擇選項：<asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </asp:Panel>
            <br />
            <asp:TextBox ID="Txt_Enterpassword" placeholder="請輸入帳戶密碼" runat="server" TextMode="Password"></asp:TextBox>
            &nbsp;
                    <asp:Button ID="Btn_check" runat="server" OnClick="Btn_check_Click" Text="簽核" />
            &nbsp;
                    <asp:Label ID="Lbl_Eorr" runat="server" ForeColor="Red" Text="密碼錯誤" Visible="False"></asp:Label>
        </asp:Panel>
        <br />

    </div>
</asp:Content>
