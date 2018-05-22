<%@ Page Title="" Language="C#" MasterPageFile="~/Management.Master" AutoEventWireup="true" CodeBehind="ProductAdd.aspx.cs" Inherits="WebApplication1.ProductAdd" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 183px;
        }
    </style>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Image1.ClientID %>');
                                                var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
                                                var reader = new FileReader();

                                                reader.onloadend = function () {
                                                    preview.src = reader.result;
                                                }

                                                if (file) {
                                                    reader.readAsDataURL(file);
                                                } else {
                                                    preview.src = "";
                                                }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">產品名稱</td>
            <td>
                <asp:TextBox placeholder="請輸入產品名稱" ID="Txt_Name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Name" ErrorMessage="*此為必要輸入欄位" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">建議售價</td>
            <td>NT $<asp:TextBox ID="Txt_Price" placeholder="請填入建議售價" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="請填入正確金額" ForeColor="Red" ValidationExpression="^(0|[1-9][0-9]*)$" ControlToValidate="Txt_Price" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_Price" ErrorMessage="*此為必要輸入欄位" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>

        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">選擇車種</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>--請選擇車款--</asp:ListItem>
                    <asp:ListItem>登山車</asp:ListItem>
                    <asp:ListItem>公路車</asp:ListItem>
                    <asp:ListItem>淑女車</asp:ListItem>
                    <asp:ListItem>折疊車</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">上傳圖檔</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" onchange="previewFile()" /><%--<asp:Button ID="Btn_UpLoad" runat="server" Text="上傳" OnClick="Btn_UpLoad_Click" ValidationGroup="Upload" />--%>
                <asp:Image ID="Image1" runat="server" Style="max-width: 500px; max-height: 250px;" />
                
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">產品介紹</td>
            <td>
                <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server"></CKEditor:CKEditorControl>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CKEditorControl1" ErrorMessage="*此為必要輸入欄位" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="Btn_Insert" runat="server" Text="新增產品" OnClick="Btn_Insert_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Btn_Return" runat="server" Text="返回" OnClick="Btn_Return_Click" ValidationGroup="NO"  />
    <asp:Label ID="Lbl_Eorr" runat="server"></asp:Label>
    <br />
    <br />
</asp:Content>
