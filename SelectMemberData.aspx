<%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SelectMemberData.aspx.cs" Inherits="WebApplication1.ManagerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="My97DatePicker/WdatePicker.js"></script>
    <style>
        /* Icon when the collapsible content is shown */
        .btn:after {
            font-family: "Glyphicons Halflings";
            content: "\e114";
            float: right;
            margin-left: 15px;
        }
        /* Icon when the collapsible content is hidden */
        .btn.collapsed:after {
            content: "\e080";
        }

        .row {
            padding: 0 10px;
        }

        .clickable {
            cursor: pointer;
        }

        .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }

        html {
            height: 100%;
        }



        .auto-style1 {
            width: 533px;
        }

        .panel panel-default{
            width: 500px
        }
    </style>
    <script>
        $(document).on('click', '.panel-heading.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            }
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>
    <br />
    <div class="panel panel-primary">
        <div class="panel-heading pull clickable">
            <h3 class="panel-title "><span>查詢<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td class="auto-style1">
                        <div class="panel panel-default" <%--style="width: 500px"--%>>
                            <div class="panel-heading">
                                <asp:TextBox ID="TB_Inquire" runat="server"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="查詢" OnClick="Button1_Click" />
                            </div>

                            <div class="panel-body" style="width:80vw">
                                <asp:GridView ID="Menu" runat="server" AutoGenerateColumns="False"  Style="border: 2px #ccc solid; border-radius: 10px;">
                                    <Columns>
                                        <asp:TemplateField HeaderText="員工編號">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Eid" runat="server" Text='<%# Eval("Eid") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="單位部門">
                                             <ItemTemplate>
                                             <asp:Label ID="Lbl_Department" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                               </ItemTemplate>
                                             <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="職稱">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_position" runat="server" Text='<%# Eval("position") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="性別">
                                             <ItemTemplate>
                                             <asp:Label ID="Lbl_Gender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                               </ItemTemplate>
                                             <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="地址">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_Address" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-Mail">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_Email" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="電話">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_Tel" runat="server" Text='<%# Eval("Tel") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="手機">
                                             <ItemTemplate>
                                             <asp:Label ID="Lbl_Cel" runat="server" Text='<%# Eval("Cel") %>'></asp:Label>
                                               </ItemTemplate>
                                             <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="生日">
                                            <ItemTemplate>
                                             <asp:Label ID="Lbl_Birthday" runat="server" Text='<%# Eval("Birthday") %>'></asp:Label>
                                               </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <EmptyDataRowStyle BorderStyle="None" Font-Size="Large" />
                                    <HeaderStyle Font-Size="Large" />
                                    <RowStyle Font-Size="Large" BackColor="White" />
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

      <div class="panel panel-primary">
        <div class="panel-heading pull clickable">
            <h3 class="panel-title "><span>新增<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td class="auto-style1">
                        <div class="panel panel-default" <%--style="width: 500px"--%>>
                            <div class="panel-heading">
                                <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="Dp">部門</asp:ListItem>
        <asp:ListItem Value="FT">公文總類</asp:ListItem>
        <asp:ListItem Value="PO">職稱</asp:ListItem>
    </asp:DropDownList>
                                 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

    <asp:Button ID="Button2" runat="server" Text="確定新增" OnClick="Button2_Click" />

    <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server" Visible="False"></asp:TextBox>
                            </div>

                            <div class="panel-body">
                                <asp:GridView ID="TG" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                             <ItemTemplate>
                                             <asp:Label ID="Lbl_TG" runat="server" Text='<%# Eval("TN") %>'></asp:Label>
                                               </ItemTemplate>
                                             <HeaderStyle Width="100px" BorderWidth="0px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>







    
    

   
   

</asp:Content>
