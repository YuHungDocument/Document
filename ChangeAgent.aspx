<%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="ChangeAgent.aspx.cs" Inherits="WebApplication1.ChangeAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style4">
        <div class="container">
            <div style="border: thin solid #C0C0C0; padding: 10px; margin: auto; background-color: #E9E9E9; width: auto; height: auto;" class="auto-style1">

                <h3>代理人設定</h3>
                <br />
                代理職務<asp:CheckBox ID="CheckBox1" runat="server" Text="發文" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="收文" />
                <br />
                代理單位<asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="TN" DataValueField="TN"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Dp" Name="Tp" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <br />
                代理人<asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="Name" DataValueField="Name" OnDataBound="DropDownList8_DataBound"></asp:DropDownList>

                <asp:DropDownList ID="DropDownList10" runat="server" DataSourceID="SqlDataSource6" DataTextField="EID" DataValueField="EID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [EID] FROM [UserInfo] WHERE ([Name] = @Name)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList8" Name="Name" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [Name] FROM [UserInfo] WHERE ([Department] = @Department)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList7" Name="Department" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                開始代理日期:
                <input type="text" name="d3" class="Wdate" id="d3" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '%y-%M-%d,%H,%m', maxDate: '#F{$dp.$D(\'d4\')}' })" />
                <br />
                <br />
                結束代理日期:
                <input type="text" name="d4" class="Wdate" id="d4" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '#F{$dp.$D(\'d3\')}' })" />
                <br />
                <br />
                <asp:Button ID="Btn_Save" class="btn btn-warning" runat="server" Text="儲存" OnClick="Btn_Save_Click" />

                &nbsp;&nbsp;&nbsp;&nbsp;

                        <asp:Button ID="back" class="btn btn-default" runat="server" Text="返回" OnClick="back_Click" />

            </div>
        </div>
    </div>
</asp:Content>
