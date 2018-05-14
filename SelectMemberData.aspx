 <%@ Page Language="C#" MasterPageFile="~/GuildPage.Master" AutoEventWireup="true" CodeBehind="SelectMemberData.aspx.cs" Inherits="WebApplication1.ManagerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/all.css" rel="stylesheet" />
    <script src="js/all.js"></script>
    <script src="My97DatePicker/WdatePicker.js"></script>
<%--    <script src="My97DatePicker/WdatePicker.js"></script>--%>
<%--    <style>
        /* Icon when the collapsible content is shown */
        .btn:after {
            font-family: "Glyphicons Halflings";
            c
            margin-left: 15px;
        }
        /* Icon when the collapsible content is hidden */
        .btn.collapsed:after {
            content: "\e080";
        }

        .row 
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

        .panel panel-default {
            width: 500px
        }
    </style>--%>
  <%--  <script>
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
    --%>
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
                            </div>

                            <div class="panel-body" style="width: 80vw">
                                <asp:GridView ID="Menu" runat="server" AutoGenerateColumns="False" Style="border: 2px #ccc solid; border-radius: 10px;">
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
    </div>ontent: "\e114";
            float: right;

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

    <div class="panel panel-primary">
        <div class="panel-heading pull clickable">
            <h3 class="panel-title "><span>給予權限<i class="glyphicon glyphicon-chevron-up"></i></span></h3>
        </div>
        <div class="panel-body">
            <table>
                <tr>
                    <td class="auto-style1">
                        <div class="panel panel-default" <%--style="width: 500px"--%>>

                            <div class="panel-heading">
                                單位<br/>
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="TN" DataValueField="TN"></asp:DropDownList><br/>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [TN] FROM [TypeGroup] WHERE ([Tp] = @Tp) ORDER BY [TID]">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="Dp" Name="Tp" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                
                                名稱<br/>
                                <asp:DropDownList ID="DDL_Name" runat="server" DataTextField="Name" DataValueField="Name" DataSourceID="SqlDataSource3" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList ID="DDL_EID" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="EID" DataValueField="EID">
                                </asp:DropDownList>
                                <br/>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [EID] FROM [UserInfo] WHERE ([Name] = @Name)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DDL_Name" Name="Name" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource><br/>
                               
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:電子公文ConnectionString %>" SelectCommand="SELECT [Name] FROM [UserInfo] WHERE ([Department] = @Department)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownList2" Name="Department" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource><br/>
                                <br />
                                將從 <input type="text" name="DS" class="Wdate" id="DS" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '%y-%M-%d,%H,%m', maxDate: '#F{$dp.$D(\'DE\')}' })" />

                               到 <input type="text" name="DE" class="Wdate" id="DE" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm', minDate: '#F{$dp.$D(\'DS\')}' })" />&nbsp; 止<br />
                                <br />
                                擁有權限<asp:DropDownList ID="DDL_Permission" runat="server">
                                    <asp:ListItem Value="1"></asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />

                            </div>
                            <div class="panel-body">

                                <br />
                                <br />
                                <asp:Button ID="Btn_Send_Permission" runat="server" Text="send" OnClick="Btn_Send_Permission_Click" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
            </table>
        </div>

    </div>











</asp:Content>
