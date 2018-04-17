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
        <asp:GridView ID="Gv_Total" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" Width="148px" OnRowDataBound="Gv_Total_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SID" Visible="False" />
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_number" runat="server" Text='<%# Bind("number") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="投票項目">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_Vname" runat="server" Text='<%# Bind("Vname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Total" HeaderText="投票數" />
            </Columns>
            <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
            <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
            <RowStyle Font-Size="Large" BackColor="White" HorizontalAlign="Center" />
        </asp:GridView>

        <asp:GridView ID="Gv_path" Style="border: 2px #ccc solid; border-radius: 10px;" runat="server" AutoGenerateColumns="False" OnRowDataBound="Gv_path_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SID" Visible="False" />
                <asp:BoundField DataField="Lvl" HeaderText="階層" />
                <asp:BoundField DataField="Department" HeaderText="部門" />
                <asp:BoundField DataField="Name" HeaderText="員工名稱" />
                <asp:TemplateField HeaderText="有無簽核">
                    <ItemTemplate>
                        <asp:Label ID="Lbl_sign" runat="server" Text='<%# Bind("sign") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="signtime" HeaderText="簽核時間" />
            </Columns>
            <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
            <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
            <RowStyle Font-Size="Large" BackColor="White" HorizontalAlign="Center" />
        </asp:GridView>
        <br />
        <asp:GridView Style="border: 2px #ccc solid; border-radius: 10px;" ID="gv_showTempFile" runat="server" AutoGenerateColumns="false" DataKeyNames="FNO">
            <Columns>
                <asp:TemplateField HeaderText="已上傳的檔案">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn_filename" runat="server" OnClick="OpenDoc" Text='<%# Eval("Name") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
            <HeaderStyle BackColor="#F2F2F2" Font-Size="Large" />
            <RowStyle Font-Size="Large" BackColor="White" HorizontalAlign="Center" />
        </asp:GridView>
        <br />
        <asp:Panel ID="Pnl_sign" runat="server" Visible="False">
            <asp:Panel ID="Pel_selectwatch" runat="server" Visible="False">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="148px" OnRowDataBound="GridView2_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="投票項次">
                            <ItemTemplate>
                                <asp:Label ID="Lb_Num" runat="server" Text='<%# Bind("number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="投票項目">
                            <ItemTemplate>
                                <asp:Label ID="Lb_Vname" runat="server" Text='<%# Bind("Vname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SID" Visible="False" />
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
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
            <br />
            <br />
            <asp:Button ID="BtnEnd" runat="server" Text="結案歸檔" OnClick="BtnEnd_Click" Visible="False" />

        </asp:Panel>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="意見及評論"></asp:Label>
        <asp:LinkButton ID="Lb_Sort" runat="server" OnClick="Lb_Sort_Click">由舊到新排序</asp:LinkButton>
        <asp:LinkButton ID="Lb_Dsort" runat="server" OnClick="Lb_Dsort_Click" Visible="False">由新到舊排序</asp:LinkButton>
        <br />
        <hr />
        <br />
        <asp:Panel ID="Pel_Comment" runat="server" Visible="False">
            <asp:TextBox ID="Txt_comment" runat="server" Height="127px" TextMode="MultiLine" Width="863px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Btn_Comment" runat="server" Text="送出評論" OnClick="Btn_Comment_Click" />
        </asp:Panel>
        <br />
        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
               <asp:Label ID="NameLabel" runat="server" Font-Bold="True" Font-Size="Large" Text='<%# Eval("Name") %>' />
                <asp:Label ID="DateLabel" runat="server" ForeColor="#999999" Text='<%# Eval("Date") %>' />
                <br />
                　　<asp:Label ID="UserCommentLabel"  Font-Size="Large" runat="server" Text='<%# Eval("UserComment") %>' />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT * FROM [Comment] WHERE ([SID] = @SID) ORDER BY [CID] DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="Lbl_SID" Name="SID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        
    </div>
</asp:Content>
