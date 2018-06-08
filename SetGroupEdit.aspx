﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SetGroupEdit.aspx.cs" Inherits="WebApplication1.SetGroupEdit" %>

<%@ Register Src="~/ucGridViewChoiceAll.ascx" TagPrefix="uc1" TagName="ucGridViewChoiceAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
                table {
            width: 100%;
            border: #ffffff 1px solid;
            border-bottom: 1px #dddddd solid;
        }
        body{
            font-size:large
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; width: 80%; height: 40%; margin: 0px auto;">
        <table class="nav-justified">
            <tr>
                <td style="width:100px">群組名稱：</td>
                <td><asp:TextBox ID="Txt_GpName" class="form-control" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="Button1" runat="server" Text="群組名稱修改" /></td>
            </tr>
        </table>
&nbsp;&nbsp; &nbsp; &nbsp;
        
        <asp:Label ID="Lbl_GID" runat="server" Visible="false"></asp:Label>
        <hr />
        <asp:GridView ID="GridView2" class="table" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" DataKeyNames="GID" OnRowCommand="GridView2_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="排列" ShowHeader="False" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("GID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SID" HeaderText="序列" Visible="false" />
                <asp:TemplateField HeaderText="層級">
                    <ItemTemplate>
                        <asp:TextBox DataField="Lvl" ID="Txt_Lvl" runat="server" OnTextChanged="Txt_Lvl_TextChanged" TextMode="Number" Width="50px" AutoPostBack="True"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="人員編號">
                    <ItemTemplate>
                        <asp:TextBox DataField="EID" placeholder="請輸入員工編號或姓名" ID="Txt_EID" runat="server" AutoPostBack="True" OnTextChanged="Txt_EID_TextChanged"></asp:TextBox>
                        <asp:Label ID="Lbl_Agent" runat="server" Text="代理人" ForeColor="Red" Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部門">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_Dep" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="員工姓名">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_Name" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll1" CheckBoxName="Cb_sign" HeaderText="需簽章" OnCheckedChanged="Cb_sign_CheckedChanged" AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Cb_sign" runat="server" OnCheckedChanged="Cb_sign_CheckedChanged" AutoPostBack="True" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll" CheckBoxName="Cb_path" HeaderText="可察看進度" OnCheckedChanged="Cb_path_CheckedChanged" AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Cb_path" runat="server" OnCheckedChanged="Cb_path_CheckedChanged" AutoPostBack="True" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <uc1:ucGridViewChoiceAll runat="server" ID="ucGridViewChoiceAll3" CheckBoxName="Cb_comment" HeaderText="是否評論" OnCheckedChanged="Cb_comment_CheckedChanged" AutoPostBack="True" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Cb_comment" runat="server" AutoPostBack="true" OnCheckedChanged="Cb_comment_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="Lbl_Delete" CommandName="DelData" OnClientClick="javascript:return confirm('確定刪除?')" runat="server">刪除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Left" />
        </asp:GridView>
        <br />
        <br />
        <div style="text-align:center">
            <asp:Button ID="Btn_Edit" runat="server" Text="修改" Font-Size="X-Large" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_Return" runat="server" Font-Size="X-Large" Text="退出" />
        </div>
    </div>

</asp:Content>
