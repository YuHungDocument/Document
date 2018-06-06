<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="SetAboutUs.aspx.cs" Inherits="WebApplication1.SetAboutUs_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <div>
        <div style="text-align: center">
            <h1 style="color: #0094ff">關於我們</h1>
        </div>

        <table class="nav-justified">
            <tr>
                <td>
                    <h2 style="font-weight: bold; color: #0094ff;">我們的團隊</h2>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 400px">
                    <asp:Image ID="Image2" Height="200px" Width="350px" ImageUrl="~/P/ncut.jpg" runat="server" />

                </td>
                <td>
                    <asp:TextBox ID="txt_title1" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#0094FF" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txt_context1" runat="server" ForeColor="#0094FF" Height="200px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <br />
    <hr />
    <div>
        <table class="nav-justified">
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: right">
                    <h2 style="font-weight: bold; color: #0094ff;">使用軟體</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_title2" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#0094FF" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    <br />
                    <br />
                    <asp:TextBox ID="txt_context2" runat="server" ForeColor="#0094FF" Height="200px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 400px">

                    <asp:Image ID="Image1" runat="server" Height="200px" Width="350px" ImageUrl="~/P/asp.net.png" />

                </td>
            </tr>
        </table>

    </div>
        <asp:Button ID="Btn_Edit" runat="server" Text="修改" OnClick="Btn_Edit_Click" />
    
</asp:Content>
