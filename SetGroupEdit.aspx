<%@ Page Title="" Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SetGroupEdit.aspx.cs" Inherits="WebApplication1.SetGroupEdit" %>

<%@ Register Src="~/ucGridViewChoiceAll.ascx" TagPrefix="uc1" TagName="ucGridViewChoiceAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            width: 100%;
            border: #ffffff 1px solid;
            border-bottom: 1px #dddddd solid;
        }

        body {
            font-size: large
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align: center; width: 100%; height: 40%; margin: 0px auto;">
        <table class="nav-justified">
            <tr>
                <td style="width: 100px">群組名稱：</td>
                <td>
                    <asp:TextBox ID="Txt_GpName" class="form-control" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="群組名稱修改" /></td>
            </tr>
        </table>
        &nbsp;&nbsp; &nbsp; &nbsp;
        
        <asp:Label ID="Lbl_GID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Lbl_GpName" runat="server" Visible="false"></asp:Label>
        <hr />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView2" class="table" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" DataKeyNames="ID" OnRowCommand="GridView2_RowCommand" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing" HorizontalAlign="Left" OnRowDataBound="GridView2_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="排列" ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("GID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SID" HeaderText="序列" Visible="false" />
                        <asp:TemplateField HeaderText="層級">
                            <ItemTemplate>
                                <asp:Label DataField="Lvl" ID="Lbl_Lvl" runat="server" Text='<%# Bind("Lvl") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox DataField="Lvl" ID="Txt_Lvl" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox DataField="Lvl" ID="Txt_Lvl2" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="人員編號">
                            <HeaderTemplate>
                                人員編號(<asp:LinkButton ID="Lb_Insert" runat="server" OnClick="Lb_Insert_Click">新增人員到此群組</asp:LinkButton>)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label DataField="EID" ID="Lbl_EID" runat="server" Text='<%# Bind("EID") %>'></asp:Label>
                                <asp:Label ID="Lbl_Agent" runat="server" Text="代理人" ForeColor="Red" Visible="False"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox DataField="EID" placeholder="請輸入員工編號或姓名" ID="Txt_EID" runat="server" AutoPostBack="True" OnTextChanged="Txt_EID_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox DataField="EID" placeholder="請輸入員工編號或姓名" ID="Txt_EID2" runat="server" AutoPostBack="True" OnTextChanged="Txt_EID_TextChanged1"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="部門">
                            <ItemTemplate>
                                <asp:Label ID="Lbl_Dep" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="Lbl_Dep2" runat="server" Text=""></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Lbl_Dep3" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="員工姓名">
                            <ItemTemplate>
                                <asp:Label ID="Lbl_Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="Lbl_Name2" runat="server" Text=""></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Lbl_Name3" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                需簽章
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Lbl_sign" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="Cb_sign" runat="server"  />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="Cb_sign2" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                可察看進度
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Lbl_path" runat="server" Text='<%# Bind("path") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="Cb_path" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="Cb_path2" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                是否評論
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Lbl_comment" runat="server" Text='<%# Bind("comment") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="Cb_comment" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="Cb_comment2" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="Lbl_Edit" runat="server" CommandName="Edit">編輯</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lcmdUpdate" runat="server" CommandName="UpdateData">更新</asp:LinkButton>
                                <asp:LinkButton ID="lcmdCancel_E" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="Lb_InsertMember" runat="server" OnClick="Lb_InsertMember_Click">新增</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="Lbl_Delete" CommandName="DelData" OnClientClick="javascript:return confirm('確定刪除?')" runat="server">刪除</asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="Lb_Canel" runat="server" OnClick="Lb_Canel_Click">取消</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Left" />
                    <RowStyle HorizontalAlign="Left" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <br />
        <div style="text-align: center">
            &nbsp;<asp:Button ID="Btn_Return" runat="server" Font-Size="X-Large" Text="退出" OnClick="Btn_Return_Click" />
        </div>
    </div>

</asp:Content>
